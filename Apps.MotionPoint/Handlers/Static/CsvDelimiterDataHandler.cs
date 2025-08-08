using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.MotionPoint.Handlers.Static;

public class CsvDelimiterDataHandler : IStaticDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData()
    {
        return new List<DataSourceItem>
        {
            new(",", "Comma (,)"),
            new(";", "Semicolon (;)"),
            new(":", "Colon (:)"),
            new("|", "Pipe (|)"),
            new("^", "Caret (^)"),
            new("\t", "Tab (\t)"),
            new("#", "Pound (#)"),
            new("%", "Percent (%)"),
            new("&", "Ampersand (&)")
        };
    }
}