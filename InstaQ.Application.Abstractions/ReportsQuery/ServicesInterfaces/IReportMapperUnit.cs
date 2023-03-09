using InstaQ.Application.Abstractions.ReportsQuery.DTOs.ReportDto;
using InstaQ.Domain.Reposts.BaseReport.Entities;

namespace InstaQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;

public interface IReportMapperUnit<out TReportDto, in TReport> where TReportDto : ReportDto where TReport : Report
{
    public TReportDto Map(TReport report);
}