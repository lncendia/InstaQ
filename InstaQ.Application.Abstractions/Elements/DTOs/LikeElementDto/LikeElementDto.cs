namespace InstaQ.Application.Abstractions.Elements.DTOs.LikeElementDto;

public class LikeElementDto : PublicationElementDto.PublicationElementDto
{
    public LikeElementDto(LikeElementBuilder builder) : base(builder)
    {
        if (builder.Likes != null) Likes.AddRange(builder.Likes);
        if (builder.Children != null) Children.AddRange(builder.Children);
    }

    public List<LikeDto> Likes { get; } = new();
    public List<LikeElementDto> Children { get; } = new();
}