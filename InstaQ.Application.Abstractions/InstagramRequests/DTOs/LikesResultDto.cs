namespace InstaQ.Application.Abstractions.InstagramRequests.DTOs;

public class LikesResultDto : ResultDto
{
    public LikesResultDto(List<string> likes, int countRequests) : base(countRequests) => Likes = likes;

    public List<string> Likes { get; }
}