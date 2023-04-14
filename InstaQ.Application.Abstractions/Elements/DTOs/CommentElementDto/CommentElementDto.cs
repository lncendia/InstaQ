namespace InstaQ.Application.Abstractions.Elements.DTOs.CommentElementDto;

public class CommentElementDto : PublicationElementDto.PublicationElementDto
{
    public CommentElementDto(string name, string pk, string likeChatName, Guid participantId, bool isAccepted,
        bool vip, string? note, IEnumerable<CommentDto> comments, IEnumerable<CommentElementDto> children) : base(name,
        pk, likeChatName, participantId, isAccepted, vip, note)
    {
        Comments.AddRange(comments);
        Children.AddRange(children);
    }

    public List<CommentDto> Comments { get; } = new();
    public List<CommentElementDto> Children { get; } = new();
}