using Newtonsoft.Json;

namespace Apps.MotionPoint.Models.Dtos;

public class AdditionalDataDto
{
    [JsonProperty("file_name")]
    public string FileName { get; set; } = string.Empty;
    
    [JsonProperty("user_additional_data")]
    public string? UserAdditionalData { get; set; }
}