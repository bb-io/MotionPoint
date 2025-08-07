using Apps.MotionPoint.Models.Requests;
using Apps.MotionPoint.Models.Responses;
using Apps.MotionPoint.Services;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.MotionPoint.Actions;

[ActionList("Job")]
public class JobActions(InvocationContext invocationContext) : Invocable(invocationContext)
{
    private readonly LanguageMappingService _languageMappingService = new(invocationContext);
    
    [Action("Search jobs", Description = "Search available jobs based on the provided criteria.")]
    public async Task<SearchJobResponse> SearchJobs([ActionParameter] SearchJobRequest searchJobRequest)
    {
        var apiRequest = new RestRequest("/translationjobs/list", Method.Post);
        if (searchJobRequest.JobStatuses != null)
        {
            var body = new
            {
                statuses = searchJobRequest.JobStatuses
            };
            
            apiRequest = apiRequest.AddJsonBody(body);
        }
        
        var queue = await _languageMappingService.GetQueueIdentifierAsync(searchJobRequest.SourceLanguage, searchJobRequest.TargetLanguage, searchJobRequest.Country);
        apiRequest.AddHeader("X-MotionCore-Queue", queue);
        var jobs = await Client.PaginateAsync<JobResponse>(apiRequest);
        return new(jobs);
    }
}