namespace Apps.MotionPoint.Models.Dtos;

public class ErrorDto
{
    public DateTime Timestamp { get; set; }
    
    public string Path { get; set; } = string.Empty;

    public long Status { get; set; }
    
    public string Error { get; set; } = string.Empty;
    
    public string RequestId { get; set; } = string.Empty;

    public override string ToString()
    {
        var errorMessage = $"{Status}: {Error}";
        if (!string.IsNullOrEmpty(Path))
        {
            errorMessage += $" at {Path}";
        }
        if (!string.IsNullOrEmpty(RequestId))
        {
            errorMessage += $" (Request ID: {RequestId})";
        }
        
        return errorMessage;
    }
}