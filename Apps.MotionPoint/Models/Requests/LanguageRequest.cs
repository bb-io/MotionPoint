using Blackbird.Applications.Sdk.Common;

namespace Apps.MotionPoint.Models.Requests;

public class LanguageRequest
{
    [Display("Source language")]
    public string SourceLanguage { get; set; } = string.Empty;
    
    [Display("Target language")]
    public string TargetLanguage { get; set; } = string.Empty;
    
    [Display("Country")]
    public string? Country { get; set; }
}