using Blackbird.Applications.Sdk.Common;

namespace Apps.MotionPoint.Models.Responses;

public class JobResponse
{
    [Display("Job ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Job status")]
    public string Status { get; set; } = string.Empty;

    [Display("Source language")]
    public string SourceLanguage { get; set; } = string.Empty;

    [Display("Target language")]
    public string TargetLanguage { get; set; } = string.Empty;

    [Display("Target country")]
    public string TargetCountry { get; set; } = string.Empty;

    [Display("Customer ID")]
    public int CustomerId { get; set; }

    [Display("Receipt date")]
    public DateTime ReceiptDate { get; set; }

    [Display("Completion date")]
    public DateTime CompletionDate { get; set; }

    [Display("Submitted by")]
    public string SubmittedBy { get; set; } = string.Empty;

    [Display("Transaction reference ID")]
    public string TransactionReferenceId { get; set; } = string.Empty;

    [DefinitionIgnore, Display("Translation job pages")]
    public List<TranslationJobPage> TranslationJobPages { get; set; } = new();

    public string GetUserFriendlyName()
    {
        return $"[{Id}] {SourceLanguage} to {TargetLanguage} ({Status})";
    }
}

public class TranslationJobPage
{
    [Display("Page ID")]
    public string Id { get; set; } = string.Empty;

    [Display("Content type")]
    public string ContentType { get; set; } = string.Empty;
}