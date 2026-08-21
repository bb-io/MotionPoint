using Newtonsoft.Json;

namespace Apps.MotionPoint.Webhooks.Models.Payload;

public class JobCallbackPayload
{
    [JsonProperty("eventId")]
    public string? EventId { get; set; }

    [JsonProperty("eventType")]
    public string? EventType { get; set; }

    [JsonProperty("eventTime")]
    public DateTime? EventTime { get; set; }

    [JsonProperty("jobId")]
    public string? JobId { get; set; }

    [JsonProperty("sourceLanguage")]
    public string? SourceLanguage { get; set; }

    [JsonProperty("targetLanguage")]
    public string? TargetLanguage { get; set; }

    /// <summary>
    /// MotionPoint spells the target language key as "tragetLanguage" in several callback payloads.
    /// </summary>
    [JsonProperty("tragetLanguage")]
    public string? MisspelledTargetLanguage { get; set; }

    [JsonProperty("targetCountry")]
    public string? TargetCountry { get; set; }

    [JsonProperty("customerId")]
    public int? CustomerId { get; set; }

    [JsonProperty("pageId")]
    public string? PageId { get; set; }

    [JsonProperty("pageUrl")]
    public string? PageUrl { get; set; }

    [JsonProperty("transactionReferenceId")]
    public string? TransactionReferenceId { get; set; }

    [JsonProperty("queueName")]
    public string? QueueName { get; set; }

    public string? ResolveTargetLanguage() => string.IsNullOrEmpty(TargetLanguage) ? MisspelledTargetLanguage : TargetLanguage;

    public bool IsTranslationCompleted() => Normalize(EventType) == "TRANSLATIONCOMPLETED";

    private static string Normalize(string? eventType) =>
        (eventType ?? string.Empty).Replace("_", string.Empty).Replace(" ", string.Empty).ToUpperInvariant();
}
