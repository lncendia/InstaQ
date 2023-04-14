using InstaQ.Application.Abstractions.Elements.DTOs.PublicationElementDto;

namespace InstaQ.Application.Abstractions.Elements.DTOs.CommentElementDto;

public class CommentElementBuilder : PublicationElementBuilder
{
    public IEnumerable<CommentElementDto>? Children { get; private set; }

    private CommentElementBuilder()
    {
    }

    public static CommentElementBuilder CommentReportElementDto => new();

    public IEnumerable<CommentDto>? Comments { get; private set; }

    public CommentElementBuilder WithComments(IEnumerable<CommentDto> comments)
    {
        Comments = comments;
        return this;
    }

    public CommentElementBuilder WithChildren(IEnumerable<CommentElementDto> children)
    {
        Children = children;
        return this;
    }

    public CommentElementDto Build()
    {
        if (string.IsNullOrEmpty(Name)) throw new InvalidOperationException("builder not formed");
        if (string.IsNullOrEmpty(Pk)) throw new InvalidOperationException("builder not formed");
        if (string.IsNullOrEmpty(LikeChatName)) throw new InvalidOperationException("builder not formed");
        if (ParticipantId == null) throw new InvalidOperationException("builder not formed");
        Comments ??= Enumerable.Empty<CommentDto>();
        Children ??= Enumerable.Empty<CommentElementDto>();

        return new CommentElementDto(Name, Pk, LikeChatName, ParticipantId.Value, IsAccepted, Vip, Note, Comments,
            Children);
    }
}