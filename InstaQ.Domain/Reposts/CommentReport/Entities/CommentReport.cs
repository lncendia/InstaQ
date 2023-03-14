using InstaQ.Domain.Links.Entities;
using InstaQ.Domain.Participants.Entities;
using InstaQ.Domain.ReportLogs.Enums;
using InstaQ.Domain.Reposts.BaseReport.Events;
using InstaQ.Domain.Reposts.BaseReport.Exceptions;
using InstaQ.Domain.Reposts.CommentReport.DTOs;
using InstaQ.Domain.Reposts.CommentReport.Exceptions;
using InstaQ.Domain.Reposts.CommentReport.ValueObjects;
using InstaQ.Domain.Reposts.PublicationReport.DTOs;
using InstaQ.Domain.Reposts.PublicationReport.Exceptions;
using InstaQ.Domain.Users.Entities;

namespace InstaQ.Domain.Reposts.CommentReport.Entities;

public class CommentReport : PublicationReport.Entities.PublicationReport
{
    public CommentReport(User user, string hashtag, bool allParticipants, int countPublicationsToGet,
        IReadOnlyCollection<Link>? coAuthors = null) : base(user, hashtag, allParticipants, countPublicationsToGet,
        coAuthors)
    {
        AddDomainEvent(new ReportCreatedEvent(LinkedUsers.Concat(new[] {UserId}), Id, ReportType.Comments,
            CreationDate, Hashtag));
    }

    public IReadOnlyCollection<CommentReportElement> Elements =>
        ReportElementsList.Cast<CommentReportElement>().ToList();

    ///<exception cref="ReportAlreadyStartedException">Report already started</exception>
    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    /// <exception cref="ParticipantNotLinkedToReportException"></exception>
    public void Start(IEnumerable<ChatParticipants> participants, IEnumerable<PublicationDto> publications)
    {
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        if (IsStarted) throw new ReportAlreadyStartedException(Id);
        var elements = new List<CommentReportElement>();

        foreach (var participantsDto in participants)
        {
            ProcessCommentChat(participantsDto.Participants.DistinctBy(x => x.Id).ToList(),
                participantsDto.LikeChatName, elements);
        }

        base.Start(publications, elements);
    }

    private void ProcessCommentChat(IList<Participant> participants, string likeChatName,
        List<CommentReportElement> elements)
    {
        var p = participants.FirstOrDefault(x => x.UserId != UserId && !LinkedUsers.Contains(x.UserId));
        if (p != null) throw new ParticipantNotLinkedToReportException(p.Id);

        var groupedElements = participants.GroupBy(x => x.ParentParticipantId).ToList();
        if (!groupedElements.Any()) return;
        var id = 1;
        foreach (var participant in groupedElements.First(x => x.Key == null))
        {
            var item = new CommentReportElement(participant.Name, likeChatName, participant.Pk, participant.Id,
                participant.Vip, participant.Notes, null, id++);
            elements.Add(item);
            var children = groupedElements.FirstOrDefault(x => x.Key == participant.Id);
            if (children == null) continue;
            elements.AddRange(children.Select(x =>
                new CommentReportElement(x.Name, likeChatName, x.Pk, x.Id, x.Vip, x.Notes, item, id++)));
        }
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ReportNotStartedException">Report not initialized</exception>
    ///<exception cref="PublicationNotFoundException">Publication not found</exception>
    ///<exception cref="CommentAlreadyExistException">Comment already exist</exception>
    public void SetComments(CommentsDto comments)
    {
        if (!IsStarted) throw new ReportNotStartedException(Id);
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        var publication =
            PublicationsList.FirstOrDefault(x => x.Pk == comments.Pk);
        if (publication == null) throw new PublicationNotFoundException();
        publication.IsLoaded = comments.SuccessLoaded;
        var nodes = ReportElementsList.Cast<CommentReportElement>();
        foreach (var node in nodes)
        {
            CommentInfo? info;
            if (comments.SuccessLoaded)
            {
                var comment = comments.Comments!.FirstOrDefault(x => x.Item1 == node.Pk);
                if (comment != default) info = new CommentInfo(publication.Id, true, comment.Item2);
                else if (publication.Pk == node.Pk) info = new CommentInfo(publication.Id, true, null);
                else info = new CommentInfo(publication.Id, false, null);
            }
            else
            {
                info = new CommentInfo(publication.Id, false, null);
            }

            node.AddComment(info);
        }

        Process++;
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    public void Finish(string? error = null)
    {
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        foreach (var element in ReportElementsList.Cast<CommentReportElement>())
        {
            if (string.IsNullOrEmpty(error) && element.Comments.Count != PublicationsList.Count)
                throw new ReportNotCompletedException(Id);
            var count = element.Comments.Count(x => x.IsConfirmed);
            if (count == PublicationsList.Count) element.Accept();
        }

        if (string.IsNullOrEmpty(error))
            Succeed();
        else
            Fail(error);
    }
}