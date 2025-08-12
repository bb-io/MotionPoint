using Blackbird.Applications.Sdk.Common.Exceptions;

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

    private static readonly Dictionary<string, string> ExtensionMap = new(StringComparer.OrdinalIgnoreCase)
    {
        { "application/xml", ".xml" },
        { "application/arb+json", ".arb" },
        { "text/html", ".html" },
        { "text/x-java-properties", ".properties" },
        { "text/javascript", ".js" },
        { "application/json", ".json" },
        { "text/strings", ".strings" },
        { "text/srt", ".srt" },
        { "text/microsoft-resx", ".resx" },
        { "text/plain", ".txt" },
        { "application/xliff+xml", ".xliff" },
        { "application/x-xliff+xml", ".xlf" },
        { "text/csv", ".csv" },
        { "image/gif", ".gif" },
        { "image/jpeg", ".jpeg" },
        { "image/jpg", ".jpg" },
        { "image/png", ".png" }
    };

    public static string GetContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        var contentType = ContentTypeMap!.GetValueOrDefault(extension, null);
        if (string.IsNullOrEmpty(contentType))
        {
            var supportedExtensions = string.Join(", ", ContentTypeMap.Keys);
            throw new PluginMisconfigurationException($"Unsupported file type '{extension}'. Supported file types: {supportedExtensions}");
        }
        
        return contentType;
    }

    public static string GetExtensionFromContentType(string contentType)
    {
        var extension = ExtensionMap!.GetValueOrDefault(contentType, null);
        if (string.IsNullOrEmpty(extension))
        {
            var supportedContentTypes = string.Join(", ", ExtensionMap.Keys);
            throw new PluginMisconfigurationException($"Unsupported content type '{contentType}'. Supported content types: {supportedContentTypes}");
        }
        
        return extension;
    }
}
