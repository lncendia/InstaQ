namespace InstaQ.Application.Abstractions.InstagramRequests.DTOs;

public class ResultDto
{
    public ResultDto(int countRequests)
    {
        CountRequests = countRequests;
    }

    public int CountRequests { get; }
}