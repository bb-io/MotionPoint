using Apps.MotionPoint.Models.Dtos;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.MotionPoint.Handlers;

public class SearchSourceLanguageDataHandler(InvocationContext invocationContext)
    : Invocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(
        DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var response = await Client.ExecuteWithErrorHandling<LanguagePairsConfiguration>(
            new RestRequest("/languages"));

        return response.LocaleData
            .Where(x => string.IsNullOrEmpty(context.SearchString) ||
                        x.SourceLanguage.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.SourceLanguage.Code, x.SourceLanguage.Name))
            .DistinctBy(x => x.Value);
    }
}
