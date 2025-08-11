using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.MotionPoint.Handlers.Static;

public class TranslationTypeDataHandler : IStaticDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData()
    {
        return new List<DataSourceItem>
        {
            new("MT", "Machine translation"),
            new("MTPE", "Machine translation and human post-editing"),
            new("MT_AUTOPE", "Machine translation and automatic post-editing"),
        };
    }
}