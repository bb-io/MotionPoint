using Apps.MotionPoint.Actions;
using Apps.MotionPoint.Models.Requests;
using Blackbird.Applications.Sdk.Common.Files;
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
        var actions = new JobActions(InvocationContext, FileManagementClient);
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
        var actions = new JobActions(InvocationContext, FileManagementClient);
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

    [TestMethod]
    public async Task CreateJob_WithRequiredFields_ReturnsJob()
    {
        var actions = new JobActions(InvocationContext, FileManagementClient);
        var request = new CreateJobRequest
        {
            SourceLanguage = "EN",
            TargetLanguage = "ES",
            Content = new FileReference
            {
                Name = "translatable.html"
            }
        };

        var response = await actions.CreateJob(request);

        Assert.IsNotNull(response);
        Assert.IsNotNull(response.Id);
        Assert.AreEqual("EN", response.SourceLanguage);
        Assert.AreEqual("ES", response.TargetLanguage);
        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
    }

    [TestMethod]
    public async Task DownloadTargetFile_WithValidJobId_ReturnsFileWithName()
    {
        var actions = new JobActions(InvocationContext, FileManagementClient);
        var request = new GetJobRequest
        {
            SourceLanguage = "EN",
            TargetLanguage = "ES",
            JobId = "7446"
        };

        var response = await actions.DownloadTargetFile(request);

        Assert.IsNotNull(response);
        Assert.IsNotNull(response.Content);
        Assert.IsNotNull(response.Content.Name);
        Assert.IsTrue(!string.IsNullOrEmpty(response.Content.Name), "FileReference.Name should not be empty.");
        Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
    }
}