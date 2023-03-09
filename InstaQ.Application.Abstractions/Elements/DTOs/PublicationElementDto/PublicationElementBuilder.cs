using InstaQ.Application.Abstractions.Elements.DTOs.ElementDto;

namespace InstaQ.Application.Abstractions.Elements.DTOs.PublicationElementDto;

public class PublicationElementBuilder : ElementBuilder
{
    public string? LikeChatName { get; private set; }
    public Guid? ParticipantId { get; private set; }
    public bool IsAccepted { get; private set; }
    public bool Vip { get; private set; }
    public string? Note { get; private set; }

    public PublicationElementBuilder WithLikeChatName(string likeChatName)
    {
        LikeChatName = likeChatName;
        return this;
    }

    public PublicationElementBuilder WithParticipantId(Guid participantId)
    {
        ParticipantId = participantId;
        return this;
    }

    public PublicationElementBuilder WithAccepted(bool isAccepted)
    {
        IsAccepted = isAccepted;
        return this;
    }

    public PublicationElementBuilder WithVip(bool isVip)
    {
        Vip = isVip;
        return this;
    }
    public PublicationElementBuilder WithNote(string note)
    {
        Note = note;
        return this;
    }
}