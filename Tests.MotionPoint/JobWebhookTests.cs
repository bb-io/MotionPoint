using Apps.MotionPoint.Models.Requests;
using Apps.MotionPoint.Webhooks;
using Apps.MotionPoint.Webhooks.Handlers;
using Apps.MotionPoint.Webhooks.Models.Requests;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Tests.MotionPoint.Base;

namespace Tests.MotionPoint;

[TestClass]
public class JobWebhookTests : TestBase
{
    private const string PayloadUrl = "https://webhook.blackbird.io/motionpoint-tests";

    private const string TranslationCompletedPayload = """
        {
            "eventId": 11,
            "eventType": "TRANSLATION COMPLETED",
            "eventTime": "2025-08-07T15:42:41.343Z",
            "jobId": 7414,
            "sourceLanguage": "EN",
            "tragetLanguage": "ES",
            "targetCountry": " ",
            "customerId": 1,
            "pageId": 823,
            "pageUrl": "http://es.client1.com/pages/client1_enes/api_new1.html",
            "transactionReferenceId": "{\"file_name\":\"translatable3.html\",\"user_additional_data\":\"my-reference\"}",
            "queueName": "EN.ES.1"
        }
        """;

    private const string InvalidContentPayload = """
        {
            "eventId": 12,
            "eventType": "INVALID_CONTENT",
            "eventTime": "2025-08-07T15:42:41.343Z",
            "jobId": 7415,
            "sourceLanguage": "EN",
            "targetLanguage": "ES",
            "customerId": 1,
            "queueName": "EN.ES.1"
        }
        """;

    [TestMethod]
    public async Task OnJobsCompleted_TranslationCompletedPayload_FliesBird()
    {
        var webhookList = new JobWebhookList(InvocationContext);

        var response = await webhookList.OnJobsCompleted(new WebhookRequest { Body = TranslationCompletedPayload },
            new OptionalCustomerRequest());

        Assert.AreEqual(WebhookRequestType.Default, response.ReceivedWebhookRequestType);
        Assert.IsNotNull(response.Result);
        Assert.AreEqual("7414", response.Result.Id);
        Assert.AreEqual("ES", response.Result.TargetLanguage);
        Assert.AreEqual("EN.ES.1", response.Result.QueueName);
        Assert.AreEqual("my-reference", response.Result.TransactionReferenceId);
        Assert.AreEqual("translatable3.html", response.Result.FileName);
        Console.WriteLine(JsonConvert.SerializeObject(response.Result, Formatting.Indented));
    }

    [TestMethod]
    public async Task OnJobsCompleted_OtherEventType_ReturnsPreflight()
    {
        var webhookList = new JobWebhookList(InvocationContext);

        var response = await webhookList.OnJobsCompleted(new WebhookRequest { Body = InvalidContentPayload },
            new OptionalCustomerRequest());

        Assert.AreEqual(WebhookRequestType.Preflight, response.ReceivedWebhookRequestType);
        Assert.IsNull(response.Result);
    }

    [TestMethod]
    public async Task OnAnyJobCompleted_NotMatchingCustomerId_ReturnsPreflight()
    {
        var webhookList = new JobWebhookList(InvocationContext);

        var response = await webhookList.OnAnyJobCompleted(new WebhookRequest { Body = TranslationCompletedPayload },
            new LanguageRequest { SourceLanguage = "EN", TargetLanguage = "ES" },
            new OptionalCustomerRequest { CustomerId = 999 });

        Assert.AreEqual(WebhookRequestType.Preflight, response.ReceivedWebhookRequestType);
        Assert.IsNull(response.Result);
    }

    [TestMethod]
    public async Task OnAnyJobCompleted_MatchingCustomerId_FliesBird()
    {
        var webhookList = new JobWebhookList(InvocationContext);

        var response = await webhookList.OnAnyJobCompleted(new WebhookRequest { Body = TranslationCompletedPayload },
            new LanguageRequest { SourceLanguage = "EN", TargetLanguage = "ES" },
            new OptionalCustomerRequest { CustomerId = 1 });

        Assert.AreEqual(WebhookRequestType.Default, response.ReceivedWebhookRequestType);
        Assert.IsNotNull(response.Result);
        Assert.AreEqual(1, response.Result.CustomerId);
    }

    [TestMethod]
    public async Task AnyJobCompletedHandler_SubscribeAndUnsubscribe_Succeeds()
    {
        var handler = new AnyJobCompletedHandler(InvocationContext,
            new LanguageRequest { SourceLanguage = "EN", TargetLanguage = "ES" });
        var values = new Dictionary<string, string> { { "payloadUrl", PayloadUrl } };

        await handler.SubscribeAsync(Credentials, values);
        await handler.UnsubscribeAsync(Credentials, values);
    }
}
