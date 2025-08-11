using Apps.MotionPoint.Handlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.MotionPoint.Base;

namespace Tests.MotionPoint;

[TestClass]
public class SourceLanguageDataHandlerTests : BaseDataHandlersTests
{
    protected override IAsyncDataSourceItemHandler DataHandler => new SourceLanguageDataHandler(InvocationContext, new());
    protected override string SearchString => "English";
}