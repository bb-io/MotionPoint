using Apps.MotionPoint.Handlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.MotionPoint.Base;

namespace Tests.MotionPoint;

[TestClass]
public class JobDataHandlerTests : BaseDataHandlersTests
{
    protected override IAsyncDataSourceItemHandler DataHandler => new JobDataHandler(InvocationContext, new()
    {
        SourceLanguage = "EN",
        TargetLanguage = "ES"
    });
    
    protected override string SearchString => "EN to ES (Completed)";

    protected override bool CanBeEmpty => true;
}