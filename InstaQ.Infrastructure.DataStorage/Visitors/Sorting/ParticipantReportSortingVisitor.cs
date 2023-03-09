using InstaQ.Domain.Ordering.Abstractions;
using InstaQ.Domain.Reposts.ParticipantReport.Entities;
using InstaQ.Domain.Reposts.ParticipantReport.Ordering.Visitor;
using InstaQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;
using InstaQ.Infrastructure.DataStorage.Visitors.Sorting.Models;

namespace InstaQ.Infrastructure.DataStorage.Visitors.Sorting;

internal class ParticipantReportSortingVisitor : BaseSortingVisitor<ParticipantReportModel, IParticipantReportSortingVisitor, ParticipantReport>, IParticipantReportSortingVisitor
{
    protected override List<SortData<ParticipantReportModel>> ConvertOrderToList(IOrderBy<ParticipantReport, IParticipantReportSortingVisitor> spec)
    {
        var visitor = new ParticipantReportSortingVisitor();
        spec.Accept(visitor);
        return visitor.SortItems;
    }
    
}