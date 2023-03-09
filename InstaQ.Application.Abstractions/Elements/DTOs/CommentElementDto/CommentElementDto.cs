namespace InstaQ.Application.Abstractions.Elements.DTOs.CommentElementDto;

public class CommentElementDto : PublicationElementDto.PublicationElementDto
{
    public CommentElementDto(CommentElementBuilder builder) : base(builder)
    {
        if (builder.Comments != null) Comments.AddRange(builder.Comments);
        if (builder.Children != null) Children.AddRange(builder.Children);
    }

    public List<CommentDto> Comments { get; } = new();
    public List<CommentElementDto> Children { get; } = new();
}