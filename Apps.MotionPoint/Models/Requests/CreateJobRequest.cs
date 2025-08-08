using Apps.MotionPoint.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.MotionPoint.Models.Requests;

public class CreateJobRequest : LanguageRequest
{
    public FileReference Content { get; set; } = new();

    public string? Comments { get; set; }

    [Display("Translation reference ID")]
    public string? TransactionReferenceId { get; set; }
    
    [Display("Translation type"), StaticDataSource(typeof(TranslationTypeDataHandler))]
    public string? TranslationType { get; set; }
}