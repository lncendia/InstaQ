using InstaQ.Application.Abstractions.Elements.DTOs.ElementDto;
using InstaQ.Domain.Reposts.BaseReport.Entities;

namespace InstaQ.Application.Abstractions.Elements.ServicesInterfaces;

public interface IElementMapperUnit<TReportElementDto, in TReportElement> where TReportElementDto : ElementDto
    where TReportElement : ReportElement
{
    public List<TReportElementDto> Map(IEnumerable<TReportElement> elements);
}