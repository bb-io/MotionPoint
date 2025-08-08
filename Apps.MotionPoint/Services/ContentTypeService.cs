namespace Apps.MotionPoint.Services;

public static class ContentTypeService
{
    private static readonly Dictionary<string, string> ContentTypeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        { ".xml", "application/xml" },
        { ".arb", "application/arb+json" },
        { ".html", "text/html" },
        { ".htm", "text/html" },
        { ".properties", "text/x-java-properties" },
        { ".js", "text/javascript" },
        { ".json", "application/json" },
        { ".strings", "text/strings" },
        { ".srt", "text/srt" },
        { ".resx", "text/microsoft-resx" },
        { ".txt", "text/plain" },
        { ".xliff", "application/xliff+xml" },
        { ".xlf", "application/x-xliff+xml" },
        { ".csv", "text/csv" },
        { ".gif", "image/gif" },
        { ".jpeg", "image/jpeg" },
        { ".jpg", "image/jpg" },
        { ".png", "image/png" }
    };

    public static string GetContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        return ContentTypeMap.GetValueOrDefault(extension, "application/octet-stream");
    }
}
