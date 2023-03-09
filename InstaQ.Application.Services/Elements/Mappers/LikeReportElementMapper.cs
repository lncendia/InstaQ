using InstaQ.Application.Abstractions.Elements.DTOs.LikeElementDto;
using InstaQ.Application.Abstractions.Elements.ServicesInterfaces;
using InstaQ.Domain.Reposts.LikeReport.Entities;

namespace InstaQ.Application.Services.Elements.Mappers;

public class LikeReportElementMapper : IElementMapperUnit<LikeElementDto, LikeReportElement>
{
    public List<LikeElementDto> Map(IEnumerable<LikeReportElement> elements)
    {
        var groupedElements = elements.GroupBy(x => x.Parent).ToList();
        if (!groupedElements.Any()) return new List<LikeElementDto>();
        return (from element in groupedElements.First(x => x.Key == null)
            let children = groupedElements.FirstOrDefault(x => x.Key == element)?.Select(x => Map(x, null))
            select Map(element, children)).ToList();
    }

    private static LikeElementDto Map(LikeReportElement element, IEnumerable<LikeElementDto>? children)
    {
        var elementBuilder = LikeElementBuilder.LikeReportElementDto;
        StaticMethods.ElementMapper.InitElementBuilder(elementBuilder, element);
        var likes = element.Likes;

        if (likes.Any())
            elementBuilder.WithLikes(likes.Select(x => new LikeDto(x.PublicationId, x.IsConfirmed)));
        if (children != null) elementBuilder.WithChildren(children);
        return elementBuilder.Build();
    }
}