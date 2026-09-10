using Apps.MotionPoint.Models.Dtos;
using Apps.MotionPoint.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.MotionPoint.Handlers;

public class SearchTargetLanguageDataHandler(
    InvocationContext invocationContext,
    [ActionParameter] SearchJobRequest searchJobRequest)
    : Invocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(
        DataSourceContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(searchJobRequest.SourceLanguage))
        {
            throw new ArgumentException("You should first input source language before fetching target languages.");
        }

        var response = await Client.ExecuteWithErrorHandling<LanguagePairsConfiguration>(
            new RestRequest("/languages"));

        return response.LocaleData
            .Where(x => x.SourceLanguage.Code == searchJobRequest.SourceLanguage)
            .Where(x => string.IsNullOrEmpty(context.SearchString) ||
                        x.TargetLanguage.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.TargetLanguage.Code, x.TargetLanguage.Name))
            .DistinctBy(x => x.Value);
    }
}
