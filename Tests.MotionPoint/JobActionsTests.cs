using Apps.MotionPoint.Actions;
using Apps.MotionPoint.Models.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Tests.MotionPoint.Base;

namespace Tests.MotionPoint;

[TestClass]
public class JobActionsTests : TestBase
{
    [TestMethod]
    public async Task SearchJobs_WithoutStatuses_ReturnsJobs()
    {
        var actions = new JobActions(InvocationContext);
        var request = new SearchJobRequest
        {
            SourceLanguage = "EN",
            TargetLanguage = "ES"
        };

        var response = await actions.SearchJobs(request);

        Assert.IsNotNull(response.Jobs);
        Assert.IsTrue(response.Jobs.Count > 0, "Expected at least one job to be returned.");
        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
    }

    [TestMethod]
    public async Task GetJob_WithValidJobId_ReturnsJob()
    {
        var actions = new JobActions(InvocationContext);
        var request = new GetJobRequest
        {
            SourceLanguage = "EN",
            TargetLanguage = "ES",
            JobId = "7414"
        };

        var response = await actions.GetJob(request);

        Assert.IsNotNull(response);
        Assert.AreEqual("7414", response.Id);
        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
    }
}