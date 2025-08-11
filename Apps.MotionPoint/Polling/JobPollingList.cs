using Apps.MotionPoint.Actions;
using Apps.MotionPoint.Api;
using Apps.MotionPoint.Models.Requests;
using Apps.MotionPoint.Models.Responses;
using Apps.MotionPoint.Polling.Models;
using Apps.MotionPoint.Services;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.MotionPoint.Polling;

[PollingEventList("Jobs")]
public class JobPollingList(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : Invocable(invocationContext)
{
    private readonly LanguageMappingService _languageMappingService = new(invocationContext);
    
    [PollingEvent("On job completed", Description = "Polling event that checks for completed job.")]
    public async Task<PollingEventResponse<DateMemory, JobCompletedResponse>> OnJobCompleted(PollingEventRequest<DateMemory> request,
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
        
        var jobActions = new JobActions(invocationContext, fileManagementClient);
        var fileResponse = await jobActions.DownloadTargetFile(jobRequest);
        return new()
        {
            FlyBird = true,
            Result = new JobCompletedResponse
            {
                Id = job.Id,
                Status = job.Status,
                SourceLanguage = job.SourceLanguage,
                TargetLanguage = job.TargetLanguage,
                TargetCountry = job.TargetCountry,
                Content = fileResponse.Content
            },
            Memory = new DateMemory
            {
                LastPollingTime = DateTime.UtcNow
            }
        };
    }
}