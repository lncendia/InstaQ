using InstaQ.WEB.ViewModels.Reports.Base;
using InstaQ.Application.Abstractions.ReportsQuery.DTOs.ReportDto;

namespace InstaQ.WEB.Mappers.Abstractions;

public interface IReportMapperUnit<in TReport, out TViewModel>
    where TReport : ReportDto where TViewModel : ReportViewModel
{
    TViewModel Map(TReport report);
}