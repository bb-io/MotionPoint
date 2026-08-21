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
    
    [Display("Callback URL", Description = "URL that receives the translation workflow event notifications of this job. Use the callback URL of the 'On jobs completed (manual)' event to get notified in Blackbird.")]
    public string? CallbackUrl { get; set; }
    
    [Display("Translation type", Description = "Translation type must be preconfigured by MotionPoint before using this parameter. Contact support to enable this feature. If not set, translation type will follow the preconfigured workflow for the project, which is normally human translation."),
        StaticDataSource(typeof(TranslationTypeDataHandler))]
    public string? TranslationType { get; set; }
    
    [Display("(JSON) Path include", Description = "One or more JSONPath selectors defining which string values will be translated")]
    public IEnumerable<string>? PathInclude { get; set; }
    
    [Display("(JSON) Path exclude", Description = "One or more JSONPath selectors defining which string values will not be translated")]
    public IEnumerable<string>? PathExclude { get; set; }
    
    [Display("(CSV) Column delimiter", Description = "A character that delimits each column’s value in the uploaded content. Default: comma (,)"), StaticDataSource(typeof(CsvDelimiterDataHandler))]
    public string? ColumnDelimiter { get; set; }
    
    [Display("(CSV) First row header", Description = "True/False. True indicates that the first row contains header and will be excluded from translation. Default:false")]
    public bool? FirstRowHeader { get; set; }
    
    [Display("(CSV) Column include", Description = "One or more column selectors defining which column data values will be translated.")]
    public IEnumerable<string>? ColumnInclude { get; set; }
    
    [Display("(CSV) Column exclude", Description = "One or more column selectors defining which column data values should not be translated.")]
    public IEnumerable<string>? ColumnExclude { get; set; }
}