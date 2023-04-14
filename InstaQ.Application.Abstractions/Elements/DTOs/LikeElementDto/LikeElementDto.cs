namespace InstaQ.Application.Abstractions.Elements.DTOs.LikeElementDto;

public class LikeElementDto : PublicationElementDto.PublicationElementDto
{
    public LikeElementDto(string name, string pk, string likeChatName, Guid participantId, bool isAccepted,
        bool vip, string? note, IEnumerable<LikeDto> likes, IEnumerable<LikeElementDto> children) : base(name,
        pk, likeChatName, participantId, isAccepted, vip, note)
    {
        Likes.AddRange(likes);
        Children.AddRange(children);
    }

    public List<LikeDto> Likes { get; } = new();
    public List<LikeElementDto> Children { get; } = new();
}