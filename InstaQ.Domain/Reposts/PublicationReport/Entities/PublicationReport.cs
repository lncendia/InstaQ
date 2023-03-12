using System.Diagnostics.CodeAnalysis;
using InstaQ.Domain.Links.Entities;
using InstaQ.Domain.Reposts.BaseReport.Entities;
using InstaQ.Domain.Reposts.BaseReport.Exceptions;
using InstaQ.Domain.Reposts.PublicationReport.DTOs;
using InstaQ.Domain.Reposts.PublicationReport.Exceptions;
using InstaQ.Domain.Users.Entities;

namespace InstaQ.Domain.Reposts.PublicationReport.Entities;

public abstract class PublicationReport : Report
{
    protected internal PublicationReport(User user, string hashtag, bool allParticipants, IReadOnlyCollection<Link>? coAuthors = null) : base(user)
    {
        if (!hashtag.StartsWith('#')) hashtag = '#' + hashtag;
        Hashtag = hashtag;
        AllParticipants = allParticipants;
        if (coAuthors == null) return;
        if (coAuthors.Count > 3) throw new TooManyLinksException();
        foreach (var l in coAuthors)
        {
            if (!l.IsConfirmed) throw new ArgumentException(null, nameof(coAuthors));
            if (l.User1Id == user.Id) _linkedUsersList.Add(l.User2Id);
            else if (l.User2Id == user.Id) _linkedUsersList.Add(l.User1Id);
            else throw new ArgumentException(null, nameof(coAuthors));
        }
    }

    private readonly List<Guid> _linkedUsersList = new();
    public IReadOnlyCollection<Guid> LinkedUsers => _linkedUsersList.AsReadOnly();
    public string Hashtag { get; }
    public int Process { get; protected set; }
    public bool AllParticipants { get; }

    protected List<Publication> PublicationsList = new();
    public IReadOnlyCollection<Publication> Publications => PublicationsList.AsReadOnly();

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    private void LoadPublications(IEnumerable<PublicationDto> publications)
    {
        var id = 1;
        PublicationsList = publications.Select(dto => new Publication(dto.Pk, dto.OwnerPk, dto.Code, id++)).ToList();
        if (!publications.Any()) throw new PublicationsListEmptyException();
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ElementsListEmptyException">elements collection is empty</exception>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    private void LoadElements(IEnumerable<PublicationReportElement> elements)
    {
        ReportElementsList.AddRange(elements);
        if(!ReportElementsList.Any()) throw new ElementsListEmptyException();
    }

    protected void Start(IEnumerable<PublicationDto> publications, IEnumerable<PublicationReportElement> elements)
    {
        if (!AllParticipants) elements = elements.Where(x => publications.Any(p => p.Pk == x.Pk));
        LoadPublications(publications);
        LoadElements(elements);
        base.Start();
    }
}