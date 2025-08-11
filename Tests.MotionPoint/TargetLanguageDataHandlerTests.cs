using Apps.MotionPoint.Handlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.MotionPoint.Base;

namespace Tests.MotionPoint;

[TestClass]
public class TargetLanguageDataHandlerTests : BaseDataHandlersTests
{
    protected override IAsyncDataSourceItemHandler DataHandler => new TargetLanguageDataHandler(InvocationContext, new() { SourceLanguage = "EN"});
    protected override string SearchString => "Spanish";
}