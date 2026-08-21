using Apps.MotionPoint.Models.Dtos;
using Apps.MotionPoint.Webhooks.Models.Payload;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.MotionPoint.Webhooks.Models.Responses;

public class JobCompletedWebhookResponse
{
    [Display("Job ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Event type")]
    public string EventType { get; set; } = string.Empty;

    [Display("Event time")]
    public DateTime? EventTime { get; set; }

    [Display("Source language")]
    public string SourceLanguage { get; set; } = string.Empty;

    [Display("Target language")]
    public string TargetLanguage { get; set; } = string.Empty;

    [Display("Target country")]
    public string TargetCountry { get; set; } = string.Empty;

    [Display("Customer ID")]
    public int? CustomerId { get; set; }

    [Display("Queue name")]
    public string QueueName { get; set; } = string.Empty;

    [Display("Page ID")]
    public string PageId { get; set; } = string.Empty;

    [Display("Page URL")]
    public string PageUrl { get; set; } = string.Empty;

    [Display("Transaction reference ID", Description = "The reference ID that was provided when the job was created.")]
    public string? TransactionReferenceId { get; set; }

    [Display("File name", Description = "The name of the file that was uploaded when the job was created.")]
    public string? FileName { get; set; }

    public JobCompletedWebhookResponse(JobCallbackPayload payload)
    {
        Id = payload.JobId ?? string.Empty;
        EventType = payload.EventType ?? string.Empty;
        EventTime = payload.EventTime;
        SourceLanguage = payload.SourceLanguage ?? string.Empty;
        TargetLanguage = payload.ResolveTargetLanguage() ?? string.Empty;
        TargetCountry = payload.TargetCountry?.Trim() ?? string.Empty;
        CustomerId = payload.CustomerId;
        QueueName = payload.QueueName ?? string.Empty;
        PageId = payload.PageId ?? string.Empty;
        PageUrl = payload.PageUrl ?? string.Empty;

        var additionalData = TryParseAdditionalData(payload.TransactionReferenceId);
        if (additionalData != null)
        {
            FileName = additionalData.FileName;
            TransactionReferenceId = additionalData.UserAdditionalData;
        }
        else
        {
            TransactionReferenceId = payload.TransactionReferenceId;
        }
    }

    private static AdditionalDataDto? TryParseAdditionalData(string? transactionReferenceId)
    {
        if (string.IsNullOrWhiteSpace(transactionReferenceId))
        {
            return null;
        }

        try
        {
            return JsonConvert.DeserializeObject<AdditionalDataDto>(transactionReferenceId);
        }
        catch (JsonException)
        {
            // Jobs created outside of Blackbird carry a plain reference ID instead of the serialized additional data.
            return null;
        }
    }
}
