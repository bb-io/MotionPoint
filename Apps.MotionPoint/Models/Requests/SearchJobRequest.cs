using Apps.MotionPoint.Handlers;
using Apps.MotionPoint.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.MotionPoint.Models.Requests;

public class SearchJobRequest
{
    [Display("Source language"), DataSource(typeof(SearchSourceLanguageDataHandler))]
    public string SourceLanguage { get; set; } = string.Empty;

    [Display("Target languages"), DataSource(typeof(SearchTargetLanguageDataHandler))]
    public IEnumerable<string> TargetLanguages { get; set; } = [];

    [Display("Country"), DataSource(typeof(SearchCountryDataHandler))]
    public string? Country { get; set; }

    [Display("Job statuses"), StaticDataSource(typeof(JobStatusDataHandler))]
    public IEnumerable<string>? JobStatuses { get; set; }

    [Display("Completed after", Description = "Return jobs completed after this date and time.")]
    public DateTime? CompletedAfter { get; set; }

    [Display("Completed before", Description = "Return jobs completed before this date and time.")]
    public DateTime? CompletedBefore { get; set; }
}