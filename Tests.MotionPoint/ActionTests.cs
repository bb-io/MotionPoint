using Apps.MotionPoint.Actions;
using Tests.MotionPoint.Base;

namespace Tests.MotionPoint;

[TestClass]
public class ActionTests : TestBase
{
    [TestMethod]
    public async Task Dynamic_handler_works()
    {
        var actions = new Actions(InvocationContext);

        await actions.Action();
    }
}
