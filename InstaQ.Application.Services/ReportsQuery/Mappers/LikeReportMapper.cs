using InstaQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using InstaQ.Domain.Reposts.LikeReport.Entities;

namespace InstaQ.Application.Services.ReportsQuery.Mappers;

public class LikeReportMapper : IReportMapperUnit<LikeReportDto, LikeReport>
{
    public LikeReportDto Map(LikeReport report)
    {
        var builder = LikeReportBuilder.LikeReportDto();
        StaticMethods.ReportMapper.InitReportBuilder(builder, report);
        var users = report.Elements.GroupBy(x => x.LikeChatName).Select(x => x.Key);
        builder.WithLinkedUsers(users).WithElements(report.Elements.Count);
        return builder.Build();
    }
}