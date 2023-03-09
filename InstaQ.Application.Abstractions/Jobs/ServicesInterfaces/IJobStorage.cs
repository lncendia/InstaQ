namespace InstaQ.Application.Abstractions.Jobs.ServicesInterfaces;

public interface IJobStorage
{
    public Task<string?> GetJobIdAsync(Guid reportId);
    public Task StoreJobIdAsync(Guid reportId, string jobId);
    public Task DeleteJobIdAsync(Guid reportId);
}