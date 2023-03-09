using InstaQ.Application.Abstractions.Elements.DTOs.CommentElementDto;
using InstaQ.Application.Abstractions.Elements.ServicesInterfaces;
using InstaQ.Domain.Reposts.CommentReport.Entities;

namespace InstaQ.Application.Services.Elements.Mappers;

public class CommentReportElementMapper : IElementMapperUnit<CommentElementDto, CommentReportElement>
{
    public List<CommentElementDto> Map(IEnumerable<CommentReportElement> elements)
    {
        var groupedElements = elements.GroupBy(x => x.Parent).ToList();
        if (!groupedElements.Any()) return new List<CommentElementDto>();
        return (from element in groupedElements.First(x => x.Key == null)
            let children = groupedElements.FirstOrDefault(x => x.Key == element)?.Select(x => Map(x, null))
            select Map(element, children)).ToList();
    }

    private static CommentElementDto Map(CommentReportElement element, IEnumerable<CommentElementDto>? children)
    {
        var elementBuilder = CommentElementBuilder.CommentReportElementDto;
        StaticMethods.ElementMapper.InitElementBuilder(elementBuilder, element);
        var comments = element.Comments;

        if (comments.Any())
            elementBuilder.WithComments(comments.Select(x =>
                new CommentDto(x.PublicationId, x.IsConfirmed, x.Text)));
        if (children != null) elementBuilder.WithChildren(children);
        return elementBuilder.Build();
    }
}