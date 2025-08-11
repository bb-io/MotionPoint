using Apps.MotionPoint.Handlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.MotionPoint.Base;

namespace Tests.MotionPoint;

[TestClass]
public class CountryDataHandlerTests : BaseDataHandlersTests
{
    protected override IAsyncDataSourceItemHandler DataHandler => new CountryDataHandler(InvocationContext, new()
    {
        SourceLanguage = "EN",
        TargetLanguage = "ES"
    });
    
    protected override string SearchString => "MEXICO";
}