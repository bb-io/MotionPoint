using Apps.MotionPoint.Models.Dtos;
using Apps.MotionPoint.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.MotionPoint.Handlers;

public class SearchCountryDataHandler(
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
            throw new ArgumentException("You should first input source language before fetching countries.");
        }

        var targetLanguages = (searchJobRequest.TargetLanguages ?? [])
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
        if (targetLanguages.Count == 0)
        {
            throw new ArgumentException("You should first input at least one target language before fetching countries.");
        }

        var response = await Client.ExecuteWithErrorHandling<LanguagePairsConfiguration>(
            new RestRequest("/languages"));

        return response.LocaleData
            .Where(x => x.SourceLanguage.Code == searchJobRequest.SourceLanguage)
            .Where(x => targetLanguages.Contains(x.TargetLanguage.Code))
            .Where(x => x.TargetLanguage.Country != null)
            .GroupBy(x => x.TargetLanguage.Country!.Code)
            .Where(x => x.Select(pair => pair.TargetLanguage.Code)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Count() == targetLanguages.Count)
            .Select(x => new DataSourceItem(x.Key, x.First().TargetLanguage.Country!.Name))
            .Where(x => string.IsNullOrEmpty(context.SearchString) ||
                        x.DisplayName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase));
    }
}
