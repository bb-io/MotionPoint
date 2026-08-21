using System.Net;
using Apps.MotionPoint.Models.Requests;
using Apps.MotionPoint.Webhooks.Handlers;
using Apps.MotionPoint.Webhooks.Models.Payload;
using Apps.MotionPoint.Webhooks.Models.Requests;
using Apps.MotionPoint.Webhooks.Models.Responses;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.MotionPoint.Webhooks;

[WebhookList("Jobs")]
public class JobWebhookList(InvocationContext invocationContext) : Invocable(invocationContext)
{
    [Webhook("On any job completed", typeof(AnyJobCompletedHandler),
        Description = "Triggered when any job of the selected language pair is completed. The callback URL for that language pair is configured in MotionPoint automatically, replacing the callback URL that was configured for it before.")]
    public Task<WebhookResponse<JobCompletedWebhookResponse>> OnAnyJobCompleted(WebhookRequest webhookRequest,
        [WebhookParameter(isSubscriptionDepends: true)] LanguageRequest languageRequest,
        [WebhookParameter] OptionalCustomerRequest customerRequest)
        => Task.FromResult(HandleJobCompletedCallback(webhookRequest, customerRequest));

    [Webhook("On jobs completed (manual)",
        Description = "Triggered when MotionPoint sends a job completion notification to this event's callback URL. The URL is shown after publishing the bird; configure it in MotionPoint yourself, or pass it to the 'Callback URL' input of the 'Create job (upload file)' action.")]
    public Task<WebhookResponse<JobCompletedWebhookResponse>> OnJobsCompleted(WebhookRequest webhookRequest,
        [WebhookParameter] OptionalCustomerRequest customerRequest)
        => Task.FromResult(HandleJobCompletedCallback(webhookRequest, customerRequest));

    private static WebhookResponse<JobCompletedWebhookResponse> HandleJobCompletedCallback(WebhookRequest webhookRequest,
        OptionalCustomerRequest customerRequest)
    {
        var payload = JsonConvert.DeserializeObject<JobCallbackPayload>(webhookRequest.Body.ToString()!);
        if (payload is null)
        {
            throw new InvalidCastException(nameof(webhookRequest.Body));
        }

        // The callback URL receives every notification event of the queue, not only completed translations.
        if (!payload.IsTranslationCompleted())
        {
            return Preflight();
        }

        if (customerRequest.CustomerId != null && customerRequest.CustomerId != payload.CustomerId)
        {
            return Preflight();
        }

        return new()
        {
            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
            Result = new JobCompletedWebhookResponse(payload)
        };
    }

    private static WebhookResponse<JobCompletedWebhookResponse> Preflight() => new()
    {
        HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK),
        Result = null,
        ReceivedWebhookRequestType = WebhookRequestType.Preflight
    };
}
