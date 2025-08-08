using Apps.MotionPoint.Api;
using Apps.MotionPoint.Models.Dtos;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.MotionPoint;

public class Invocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Credentials =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected ApiClient Client { get; }

    protected Invocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Credentials.ToList());
    }
    
    protected async Task<TranslationStatisticsDto> GetJobStatisticsAsync(string jobId, string queue)
    {
        var statisticsRequest = new ApiRequest($"/translationjobstats/jobs/{jobId}", queue, Method.Post);
        statisticsRequest.AddHeader("Content-Type", "application/json");
        return await Client.ExecuteWithErrorHandling<TranslationStatisticsDto>(statisticsRequest);
    }
}
