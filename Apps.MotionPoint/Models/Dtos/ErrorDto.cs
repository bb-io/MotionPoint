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
        return $"{Status}: {Error}; Request ID: {RequestId}";
    }
}