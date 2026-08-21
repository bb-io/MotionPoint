using Apps.MotionPoint.Api;
using Apps.MotionPoint.Models.Requests;
using Apps.MotionPoint.Services;
using Apps.MotionPoint.Webhooks.Models.Payload;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.MotionPoint.Webhooks.Handlers;

public class AnyJobCompletedHandler(InvocationContext invocationContext, [WebhookParameter(true)] LanguageRequest languageRequest)
    : BaseInvocable(invocationContext), IWebhookEventHandler
{
    private const string CallbackUrlEndpoint = "/callbackurl";

    public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider,
        Dictionary<string, string> values)
    {
        var credentials = authenticationCredentialsProvider.ToList();
        var queue = await GetQueueAsync(credentials);

        await ConfigureCallbackUrlAsync(credentials, queue, values["payloadUrl"], active: true);
    }

    public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider,
        Dictionary<string, string> values)
    {
        var credentials = authenticationCredentialsProvider.ToList();
        var queue = await GetQueueAsync(credentials);

        var configuredCallback = await GetCallbackUrlAsync(credentials, queue);
        if (configuredCallback?.CallbackUrl != values["payloadUrl"])
        {
            // The queue points at another callback URL already; leave that configuration alone.
            return;
        }

        await ConfigureCallbackUrlAsync(credentials, queue, values["payloadUrl"], active: false);
    }

    private async Task ConfigureCallbackUrlAsync(List<AuthenticationCredentialsProvider> credentials, string queue, string callbackUrl, bool active)
    {
        var client = new ApiClient(credentials);
        var request = new ApiRequest(CallbackUrlEndpoint, queue, Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddStringBody(JsonConvert.SerializeObject(new CallbackUrlDto
        {
            CallbackUrl = callbackUrl,
            Active = active
        }), DataFormat.Json);

        await client.ExecuteWithErrorHandling(request);
    }

    private async Task<CallbackUrlDto?> GetCallbackUrlAsync(List<AuthenticationCredentialsProvider> credentials, string queue)
    {
        var client = new ApiClient(credentials);
        var request = new ApiRequest(CallbackUrlEndpoint, queue);

        try
        {
            var response = await client.ExecuteWithErrorHandling(request);
            return string.IsNullOrWhiteSpace(response.Content)
                ? null
                : JsonConvert.DeserializeObject<CallbackUrlDto>(response.Content);
        }
        catch (Exception)
        {
            // No callback URL has been configured for the queue yet.
            return null;
        }
    }

    private Task<string> GetQueueAsync(List<AuthenticationCredentialsProvider> credentials)
    {
        var languageMappingService = new LanguageMappingService(credentials);
        return languageMappingService.GetQueueIdentifierAsync(languageRequest.SourceLanguage, languageRequest.TargetLanguage, languageRequest.Country);
    }
}
