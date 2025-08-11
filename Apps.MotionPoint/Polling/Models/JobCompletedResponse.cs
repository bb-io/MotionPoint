using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.MotionPoint.Polling.Models;

public class JobCompletedResponse
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
    
    public FileReference Content { get; set; } = new();
}