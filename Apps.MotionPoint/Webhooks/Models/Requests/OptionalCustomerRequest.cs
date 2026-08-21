using Blackbird.Applications.Sdk.Common;

namespace Apps.MotionPoint.Webhooks.Models.Requests;

public class OptionalCustomerRequest
{
    [Display("Customer ID", Description = "Only trigger for jobs that belong to this customer. When left empty, jobs of every customer trigger the event.")]
    public int? CustomerId { get; set; }
}
