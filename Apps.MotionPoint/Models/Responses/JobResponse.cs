using Blackbird.Applications.Sdk.Common;

namespace Apps.MotionPoint.Models.Responses;

public class JobResponse
{
    [Display("Job ID")]
    public string Id { get; set; } = string.Empty;
    
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

    [Display("Status")]
    public string Status { get; set; } = string.Empty;

    [Display("Submitted by")]
    public string SubmittedBy { get; set; } = string.Empty;
}