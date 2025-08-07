using Apps.MotionPoint.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.MotionPoint.Models.Requests;

public class LanguageRequest
{
    [Display("Source language"), DataSource(typeof(SourceLanguageDataHandler))]
    public string SourceLanguage { get; set; } = string.Empty;
    
    [Display("Target language"), DataSource(typeof(TargetLanguageDataHandler))]
    public string TargetLanguage { get; set; } = string.Empty;
    
    [Display("Country"), DataSource(typeof(CountryDataHandler))]
    public string? Country { get; set; }
}