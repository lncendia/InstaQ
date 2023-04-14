using InstaQ.Application.Abstractions.Elements.DTOs.PublicationElementDto;

namespace InstaQ.Application.Abstractions.Elements.DTOs.LikeElementDto;

public class LikeElementBuilder : PublicationElementBuilder
{
    public IEnumerable<LikeElementDto>? Children { get; private set; }

    private LikeElementBuilder()
    {
    }

    public static LikeElementBuilder LikeReportElementDto => new();

    public IEnumerable<LikeDto>? Likes;

    public LikeElementBuilder WithLikes(IEnumerable<LikeDto> likes)
    {
        Likes = likes;
        return this;
    }

    public LikeElementBuilder WithChildren(IEnumerable<LikeElementDto> children)
    {
        Children = children;
        return this;
    }

    public LikeElementDto Build()
    {
        if (string.IsNullOrEmpty(Name)) throw new InvalidOperationException("builder not formed");
        if (string.IsNullOrEmpty(Pk)) throw new InvalidOperationException("builder not formed");
        if (string.IsNullOrEmpty(LikeChatName)) throw new InvalidOperationException("builder not formed");
        if (ParticipantId == null) throw new InvalidOperationException("builder not formed");
        Likes ??= Enumerable.Empty<LikeDto>();
        Children ??= Enumerable.Empty<LikeElementDto>();

        return new LikeElementDto(Name, Pk, LikeChatName, ParticipantId.Value, IsAccepted, Vip, Note, Likes,
            Children);
    }
}