namespace InstaQ.Application.Abstractions.InstagramRequests.DTOs;

public class CommentsResultDto : ResultDto
{
    public CommentsResultDto(List<(string, string)> comments, int countRequests) : base(countRequests)
    {
        Comments = comments;
    }

    public List<(string, string)> Comments { get; }
}