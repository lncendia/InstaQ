using InstaQ.Application.Abstractions.ReportsQuery.DTOs.ReportDto;

namespace InstaQ.Application.Abstractions.ReportsQuery.DTOs.PublicationReportDto;

public abstract class PublicationReportBuilder : ReportBuilder
{
    public IEnumerable<string>? LinkedUsers { get; private set; }
    public string? Hashtag { get; private set; }
    public int PublicationsCount { get; private set; }
    public int Process { get; private set; }
    public bool AllParticipants { get; private set; }

    public PublicationReportBuilder WithLinkedUsers(IEnumerable<string> linkedUsers)
    {
        LinkedUsers = linkedUsers;
        return this;
    }

    public PublicationReportBuilder WithHashtag(string hashtag)
    {
        Hashtag = hashtag;
        return this;
    }
    

    public PublicationReportBuilder WithPublicationsCount(int count)
    {
        PublicationsCount = count;
        return this;
    }

    public PublicationReportBuilder WithProcess(int process)
    {
        Process = process;
        return this;
    }

    public PublicationReportBuilder WithAllParticipantsOption()
    {
        AllParticipants = true;
        return this;
    }
}