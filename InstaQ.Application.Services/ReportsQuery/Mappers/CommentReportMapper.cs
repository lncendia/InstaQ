using InstaQ.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;
using InstaQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using InstaQ.Domain.Reposts.CommentReport.Entities;

namespace InstaQ.Application.Services.ReportsQuery.Mappers;

public class CommentReportMapper : IReportMapperUnit<CommentReportDto, CommentReport>
{
    public CommentReportDto Map(CommentReport report)
    {
        var builder = CommentReportBuilder.CommentReportDto();
        StaticMethods.ReportMapper.InitReportBuilder(builder, report);
        var users = report.Elements.GroupBy(x => x.LikeChatName).Select(x => x.Key);
        builder.WithLinkedUsers(users).WithElements(report.Elements.Count);
        return builder.Build();
    }
}