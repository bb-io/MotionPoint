using Apps.MotionPoint.Models.Dtos;
using Apps.MotionPoint.Models.Enums;
using Apps.MotionPoint.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.MotionPoint.Handlers.Base;

public abstract class LanguageBaseDataHandler(InvocationContext invocationContext, [ActionParameter] LanguageRequest languageRequest) 
    : Invocable(invocationContext), IAsyncDataSourceItemHandler
{
    protected abstract LanguageRole Language { get; }
    
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var apiRequest = new RestRequest("/languages");
        var response = await Client.ExecuteWithErrorHandling<LanguagePairsConfiguration>(apiRequest);
        
        if(Language == LanguageRole.Source)
        {
            return response.LocaleData
                .Where(x => string.IsNullOrEmpty(context.SearchString) || x.SourceLanguage.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .Select(x => new DataSourceItem(x.SourceLanguage.Code, x.SourceLanguage.Name))
                .DistinctBy(x => x.Value);
        }
        
        if (string.IsNullOrEmpty(languageRequest.SourceLanguage))
        {
            throw new ArgumentException("You should first input source language before fetching target languages.");
        }
        
        return response.LocaleData.Where(x => x.SourceLanguage.Code == languageRequest.SourceLanguage)
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.TargetLanguage.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.TargetLanguage.Code, x.TargetLanguage.Name))
            .DistinctBy(x => x.Value);
    }
}
