using Apps.MotionPoint.Api;
using Apps.MotionPoint.Models.Dtos;
using Apps.MotionPoint.Models.Requests;
using Apps.MotionPoint.Models.Responses;
using Apps.MotionPoint.Services;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.MotionPoint.Actions;

[ActionList("Job")]
public class JobActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : Invocable(invocationContext)
{
    private readonly LanguageMappingService _languageMappingService = new(invocationContext);
    
    [Action("Search jobs", Description = "Search available jobs based on the provided criteria.")]
    public async Task<SearchJobResponse> SearchJobs([ActionParameter] SearchJobRequest searchJobRequest)
    {
        var queue = await _languageMappingService.GetQueueIdentifierAsync(searchJobRequest.SourceLanguage, searchJobRequest.TargetLanguage, searchJobRequest.Country);
        var apiRequest = new ApiRequest("/translationjobs/list", queue, Method.Post);
        if (searchJobRequest.JobStatuses != null)
        {
            var body = new
            {
                statuses = searchJobRequest.JobStatuses
            };
            
            apiRequest.AddJsonBody(body);
        }
        
        var jobs = await Client.PaginateAsync<JobResponse>(apiRequest);
        return new(jobs);
    }
    
    [Action("Get job", Description = "Retrieve details of a specific job by its ID.")]
    public async Task<FullJobResponse> GetJob([ActionParameter] GetJobRequest jobRequest)
    {
        var queue = await _languageMappingService.GetQueueIdentifierAsync(jobRequest.SourceLanguage, jobRequest.TargetLanguage, jobRequest.Country);
        var apiRequest = new ApiRequest($"/translationjobs/{jobRequest.JobId}", queue, Method.Post);
        apiRequest.AddHeader("Content-Type", "application/json");
        
        var job = await Client.ExecuteWithErrorHandling<JobResponse>(apiRequest);
        
        var statistics = await GetJobStatisticsAsync(job.Id, queue);
        return new FullJobResponse(job, statistics.TranslationStatistics);
    }

    [Action("Create job", Description = "Create a new translation job with the specified details.")]
    public async Task<JobResponse> CreateJob([ActionParameter] CreateJobRequest createJobRequest)
    {
        var queue = await _languageMappingService.GetQueueIdentifierAsync(createJobRequest.SourceLanguage, createJobRequest.TargetLanguage, createJobRequest.Country);
        var apiRequest = new ApiRequest("/translationjobs", queue, Method.Post)
        {
            AlwaysMultipartFormData = true
        };

        var stream = await fileManagementClient.DownloadAsync(createJobRequest.Content);
        var fileStream = new MemoryStream();
        await stream.CopyToAsync(fileStream);
        fileStream.Position = 0;
        
        var fileBytes = await fileStream.GetByteData();
        apiRequest.AddFile("file", fileBytes, createJobRequest.Content.Name);
        
        var contentType = ContentTypeService.GetContentType(createJobRequest.Content.Name);
        apiRequest.AddParameter("contentType", contentType);
        
        if (!string.IsNullOrEmpty(createJobRequest.TransactionReferenceId))
        {
            apiRequest.AddParameter("transactionReferenceId", createJobRequest.TransactionReferenceId);
        }
        
        if (!string.IsNullOrEmpty(createJobRequest.Comments))
        {
            apiRequest.AddParameter("comments", createJobRequest.Comments);
        }
        
        if (!string.IsNullOrEmpty(createJobRequest.TranslationType))
        {
            apiRequest.AddParameter("translationType", createJobRequest.TranslationType);
        }
        
        var response = await Client.ExecuteWithErrorHandling<JobResponse>(apiRequest);
        return response;
    }
    
    [Action("Cancel job", Description = "Cancel a specific job by its ID.")]
    public async Task CancelJob([ActionParameter] GetJobRequest jobRequest)
    {
        var queue = await _languageMappingService.GetQueueIdentifierAsync(jobRequest.SourceLanguage, jobRequest.TargetLanguage, jobRequest.Country);
        var apiRequest = new ApiRequest($"/translationjobs/{jobRequest.JobId}/cancel", queue, Method.Post);
        await Client.ExecuteWithErrorHandling(apiRequest);
    }
    
    private async Task<TranslationStatisticsDto> GetJobStatisticsAsync(string jobId, string queue)
    {
        var statisticsRequest = new ApiRequest($"/translationjobstats/jobs/{jobId}", queue, Method.Post);
        statisticsRequest.AddHeader("Content-Type", "application/json");
        return await Client.ExecuteWithErrorHandling<TranslationStatisticsDto>(statisticsRequest);
    }
}