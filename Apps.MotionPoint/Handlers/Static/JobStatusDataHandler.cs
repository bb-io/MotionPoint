using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.MotionPoint.Handlers.Static;

public class JobStatusDataHandler : IStaticDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData()
    {
        return new List<DataSourceItem>
        {
            new("QUEUED", "Queued"),
            new("ON_HOLD", "On hold"),
            new("COMPLETED", "Completed"),
            new("INVALID_CONTENT", "Invalid content")
        };
    }
}
