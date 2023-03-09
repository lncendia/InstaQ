namespace InstaQ.Application.Abstractions.Elements.DTOs.PublicationElementDto;

public abstract class PublicationElementDto : ElementDto.ElementDto
{
    protected PublicationElementDto(PublicationElementBuilder builder) : base(builder)
    {
        LikeChatName = builder.LikeChatName ?? throw new ArgumentException("builder not formed", nameof(builder));
        ParticipantId = builder.ParticipantId ?? throw new ArgumentException("builder not formed", nameof(builder));
        IsAccepted = builder.IsAccepted;
        Vip = builder.Vip;
        Note = builder.Note;
    }

    public string LikeChatName { get; }
    public Guid ParticipantId { get; }
    public bool IsAccepted { get; }
    public bool Vip { get; }
    public string? Note { get; }
}