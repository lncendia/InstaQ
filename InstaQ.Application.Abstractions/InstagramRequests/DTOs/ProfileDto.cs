namespace InstaQ.Application.Abstractions.InstagramRequests.DTOs;

public class ProfileDto
{
    public ProfileDto(string pk, string fullName, bool isPrivate, int followersCount, int followingsCount)
    {
        Pk = pk;
        FullName = fullName;
        IsPrivate = isPrivate;
        FollowersCount = followersCount;
        FollowingsCount = followingsCount;
    }

    public string Pk { get; }
    public string FullName { get; }
    public bool IsPrivate { get; }
    public int FollowersCount { get; }
    public int FollowingsCount { get; }
}