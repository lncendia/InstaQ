using InstaQ.Application.Abstractions.ReportsQuery.DTOs.PublicationReportDto;

namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;

public class CommentReportBuilder : PublicationReportBuilder
{
    private CommentReportBuilder()
    {
    }

    public static CommentReportBuilder CommentReportDto() => new();

    public CommentReportDto Build() => new(this);
}