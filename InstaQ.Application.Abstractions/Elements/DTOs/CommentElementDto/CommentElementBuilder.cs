using InstaQ.Application.Abstractions.Elements.DTOs.PublicationElementDto;

namespace InstaQ.Application.Abstractions.Elements.DTOs.CommentElementDto;

public class CommentElementBuilder : PublicationElementBuilder
{
    public IEnumerable<CommentElementDto>? Children { get; private set; }

    private CommentElementBuilder()
    {
    }

    public static CommentElementBuilder CommentReportElementDto => new();

    public IEnumerable<CommentDto>? Comments;

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

    public CommentElementDto Build() => new(this);
}