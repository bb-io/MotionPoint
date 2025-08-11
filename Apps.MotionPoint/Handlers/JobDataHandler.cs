using Apps.MotionPoint.Actions;
using Apps.MotionPoint.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.MotionPoint.Handlers;

public class JobDataHandler(InvocationContext invocationContext, [ActionParameter] LanguageRequest languageRequest) 
    : Invocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(languageRequest.SourceLanguage))
        {
            throw new ArgumentException("You should first input source language before fetching countries.");
        }
        
        if (string.IsNullOrEmpty(languageRequest.TargetLanguage))
        {
            throw new ArgumentException("You should first input target language before fetching countries.");
        }
        
        var jobActions = new JobActions(invocationContext, null!);
        var jobs = await jobActions.SearchJobs(new SearchJobRequest
        {
            SourceLanguage = languageRequest.SourceLanguage,
            TargetLanguage = languageRequest.TargetLanguage,
            Country = languageRequest.Country
        });
        
        return jobs.Jobs
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.GetUserFriendlyName().Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.Id, x.GetUserFriendlyName()))
            .DistinctBy(x => x.Value);
    }
}