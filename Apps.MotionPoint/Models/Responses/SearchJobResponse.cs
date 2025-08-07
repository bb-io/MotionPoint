using Blackbird.Applications.Sdk.Common;

namespace Apps.MotionPoint.Models.Responses;

public class SearchJobResponse(List<JobResponse> jobs)
{
    public List<JobResponse> Jobs { get; set; } = jobs;
    
    [Display("Total count")]
    public int TotalCount { get; set; } = jobs.Count;
}