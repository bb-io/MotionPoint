using Newtonsoft.Json;

namespace Apps.MotionPoint.Webhooks.Models.Payload;

public class CallbackUrlDto
{
    [JsonProperty("callbackUrl")]
    public string? CallbackUrl { get; set; }

    [JsonProperty("active")]
    public bool Active { get; set; }
}
