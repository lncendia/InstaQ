namespace InstaQ.Application.Abstractions.InstagramRequests.DTOs;

public class PublicationsResultDto : ResultDto
{
    public PublicationsResultDto(List<PublicationDto> publications, int countRequests) : base(countRequests)
    {
        Publications = publications;
    }

    public List<PublicationDto> Publications { get; }
}

public class PublicationDto
{
    public PublicationDto(string pk, string ownerPk, string code, long likesCount, long commentsCount, bool commentsDisabled)
    {
        Pk = pk;
        OwnerPk = ownerPk;
        Code = code;
        LikesCount = likesCount;
        CommentsCount = commentsCount;
        CommentsDisabled = commentsDisabled;
    }

    public string Pk { get; }
    public string OwnerPk { get; }
    public string Code { get; }
    public long LikesCount { get; }
    public long CommentsCount { get; }
    public bool CommentsDisabled { get; }
}