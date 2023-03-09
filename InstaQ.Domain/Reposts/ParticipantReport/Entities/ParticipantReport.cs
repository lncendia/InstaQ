using InstaQ.Domain.Participants.Entities;
using InstaQ.Domain.ReportLogs.Enums;
using InstaQ.Domain.Reposts.BaseReport.Entities;
using InstaQ.Domain.Reposts.BaseReport.Events;
using InstaQ.Domain.Reposts.BaseReport.Exceptions;
using InstaQ.Domain.Reposts.ParticipantReport.Enums;
using InstaQ.Domain.Reposts.ParticipantReport.Events;
using InstaQ.Domain.Reposts.ParticipantReport.Exceptions;
using InstaQ.Domain.Users.Entities;
using InstaQ.Domain.Users.Enums;

namespace InstaQ.Domain.Reposts.ParticipantReport.Entities;

public class ParticipantReport : Report
{
    public ParticipantReport(User user) : base(user)
    {
        Pk = user.Target?.Pk ?? throw new UserTargetException();
        Type = user.Target.ParticipantsType;
        AddDomainEvent(new ReportCreatedEvent(new[] {UserId}, Id, ReportType.Participants, CreationDate, "-"));
    }

    public string Pk { get; }
    public ParticipantsType Type { get; }

    public IReadOnlyCollection<ParticipantReportElement> Participants =>
        ReportElementsList.Cast<ParticipantReportElement>().ToList();

    /// <exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    /// <exception cref="ReportNotStartedException">Report not initialized</exception>
    public void ProcessParticipantInfo(string pk, string name)
    {
        if (!IsStarted) throw new ReportNotStartedException(Id);
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        var node = ReportElementsList.Cast<ParticipantReportElement>().FirstOrDefault(x => x.Pk == pk);
        if (node != null)
        {
            if (node.Name != name) node.SetType(ElementType.Rename, name);
            else node.SetType(ElementType.Stay);
        }
        else
        {
            var id = ReportElementsList.LastOrDefault()?.Id ?? 0;
            var item = new ParticipantReportElement(name, pk, null, null, id + 1);
            item.SetType(ElementType.New);
            ReportElementsList.Add(item);
        }
    }


    /// <exception cref="ReportAlreadyStartedException">Report already started</exception>
    /// <exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    /// <exception cref="ParticipantNotLinkedToReportException"></exception>
    public void Start(IList<Participant> participants)
    {
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        if (IsStarted) throw new ReportAlreadyStartedException(Id);

        var p = participants.FirstOrDefault(x => x.UserId != UserId);
        if (p != null) throw new ParticipantNotLinkedToReportException(p.Id);
        var grouperElements = participants.GroupBy(x => x.ParentParticipantId).ToList();

        if (grouperElements.Any())
        {
            int id = 1;
            foreach (var participant in grouperElements.First(x => x.Key == null))
            {
                var item = new ParticipantReportElement(participant.Name, participant.Pk, participant.Id, null, id++);
                ReportElementsList.Add(item);
                var children = grouperElements.FirstOrDefault(x => x.Key == participant.Id);
                if (children == null) continue;
                ReportElementsList.AddRange(children.Select(x =>
                    new ParticipantReportElement(x.Name, x.Pk, x.Id, item, id++)));
            }
        }

        base.Start();
    }

    /// <exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    /// <exception cref="ReportNotStartedException">Report not initialized</exception>
    public void Finish(string? error = null)
    {
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        if (!string.IsNullOrEmpty(error))
        {
            ReportElementsList.Clear();
            Fail(error);
            return;
        }

        var grouperElements = ReportElementsList.Cast<ParticipantReportElement>().GroupBy(x => x.Parent).ToList();
        if (grouperElements.Any())
        {
            foreach (var participant in grouperElements.First(x => x.Key == null))
            {
                var children = grouperElements.FirstOrDefault(x => x.Key == participant);
                var allChildrenStay = true;
                if (children != null)
                {
                    foreach (var child in children)
                    {
                        if (!child.Type.HasValue) child.SetType(ElementType.Leave);
                        else if (child.Type == ElementType.Stay) ReportElementsList.Remove(child);
                        allChildrenStay = allChildrenStay && child.Type == ElementType.Stay;
                    }
                }

                if (!participant.Type.HasValue) participant.SetType(ElementType.Leave);
                else if (participant.Type == ElementType.Stay && allChildrenStay)
                    ReportElementsList.Remove(participant);
            }
        }

        Succeed();
        var participants = ReportElementsList.Cast<ParticipantReportElement>().Select(x =>
            new ParticipantReportFinishedEvent.ParticipantDto(x.ParticipantId, x.NewName ?? x.Name, x.Pk,
                x.Type!.Value));

        AddDomainEvent(new ParticipantReportFinishedEvent(Id, UserId, participants));
    }
}