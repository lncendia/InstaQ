using InstaQ.Application.Abstractions.Elements.DTOs.PublicationElementDto;

namespace InstaQ.Application.Abstractions.Elements.DTOs.LikeElementDto;

public class LikeReportElementsDto
{
    public LikeReportElementsDto(List<LikeElementDto> elements, List<PublicationDto> publications)
    {
        Elements = elements;
        Publications = publications;
    }

    public List<LikeElementDto> Elements { get; }
    public List<PublicationDto> Publications { get; }
}