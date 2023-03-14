namespace InstaQ.Application.Abstractions.ReportsManagement.DTOs;

public class PublicationReportCreateDto
{
    public PublicationReportCreateDto(Guid userId, string hashtag, bool allParticipants, int countPublicationsToGet,
        List<Guid>? coAuthors)
    {
        UserId = userId;
        Hashtag = hashtag;
        CoAuthors = coAuthors;
        CountPublicationsToGet = countPublicationsToGet;
        AllParticipants = allParticipants;
    }

    public Guid UserId { get; }
    public string Hashtag { get; }
    public bool AllParticipants { get; }
    public int CountPublicationsToGet { get; }
    public List<Guid>? CoAuthors { get; }
}