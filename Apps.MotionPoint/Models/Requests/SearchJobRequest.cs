using Apps.MotionPoint.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.MotionPoint.Models.Requests;

public class SearchJobRequest : LanguageRequest
{
    [Display("Job statuses"), StaticDataSource(typeof(JobStatusDataHandler))]
    public IEnumerable<string>? JobStatuses { get; set; }
}