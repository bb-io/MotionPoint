using Apps.MotionPoint.Api;
using Apps.MotionPoint.Models.Requests;
using Apps.MotionPoint.Models.Responses;
using Apps.MotionPoint.Polling.Models;
using Apps.MotionPoint.Services;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using RestSharp;

namespace Apps.MotionPoint.Polling;

[PollingEventList("Jobs")]
public class JobPollingList(InvocationContext invocationContext) : Invocable(invocationContext)
{
    private readonly LanguageMappingService _languageMappingService = new(invocationContext);
    
    [PollingEvent("On job completed", Description = "Polling event that checks for completed job.")]
    public async Task<PollingEventResponse<DateMemory, FullJobResponse>> OnJobCompleted(PollingEventRequest<DateMemory> request,
        [PollingEventParameter] GetJobRequest jobRequest)
    {
        var queue = await _languageMappingService.GetQueueIdentifierAsync(jobRequest.SourceLanguage, jobRequest.TargetLanguage, jobRequest.Country);
        var apiRequest = new ApiRequest($"/translationjobs/{jobRequest.JobId}", queue, Method.Post);
        apiRequest.AddHeader("Content-Type", "application/json");
        
        var job = await Client.ExecuteWithErrorHandling<JobResponse>(apiRequest);
        if (job.Status != "COMPLETED" || job.CompletionDate < request.Memory?.LastPollingTime)
        {
            return new()
            {
                FlyBird = false,
                Result = null!,
                Memory = new DateMemory
                {
                    LastPollingTime = DateTime.UtcNow
                }
            };
        }
        
        var statistics = await GetJobStatisticsAsync(job.Id, queue);
        return new()
        {
            FlyBird = true,
            Result = new FullJobResponse(job, statistics.TranslationStatistics),
            Memory = new DateMemory
            {
                LastPollingTime = DateTime.UtcNow
            }
        };
    }
}