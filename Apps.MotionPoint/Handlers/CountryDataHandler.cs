using Apps.MotionPoint.Models.Dtos;
using Apps.MotionPoint.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.MotionPoint.Handlers;

public class CountryDataHandler(InvocationContext invocationContext, [ActionParameter] LanguageRequest languageRequest) 
    : Invocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var apiRequest = new RestRequest("/languages");
        var response = await Client.ExecuteWithErrorHandling<LanguagePairsConfiguration>(apiRequest);

        if (string.IsNullOrEmpty(languageRequest.SourceLanguage))
        {
            throw new ArgumentException("You should first input source language before fetching countries.");
        }
        
        if (string.IsNullOrEmpty(languageRequest.TargetLanguage))
        {
            throw new ArgumentException("You should first input target language before fetching countries.");
        }
        
        return response.LocaleData
            .Where(x => x.SourceLanguage.Code == languageRequest.SourceLanguage)
            .Where(x => x.TargetLanguage.Code == languageRequest.TargetLanguage)
            .Where(x => x.TargetLanguage.Country != null)
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.TargetLanguage.Country!.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.TargetLanguage.Country!.Code, x.TargetLanguage.Country.Name))
            .DistinctBy(x => x.Value);
    }
}