using Apps.MotionPoint.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.MotionPoint.Models.Requests;

public class GetJobRequest : LanguageRequest
{
    [Display("Job ID", Description = "The unique identifier of the job to retrieve."), DataSource(typeof(JobDataHandler))]
    public string JobId { get; set; } = string.Empty;
}