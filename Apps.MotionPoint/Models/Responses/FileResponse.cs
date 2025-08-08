using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.MotionPoint.Models.Responses;

public class FileResponse(FileReference fileReference)
{
    public FileReference Content { get; set; } = fileReference;
}