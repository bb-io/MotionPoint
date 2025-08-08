using RestSharp;

namespace Apps.MotionPoint.Api;

public class ApiRequest : RestRequest
{
    public ApiRequest(string endpoint, string queue, Method method = Method.Get) : base(endpoint, method)
    {
        this.AddHeader("X-MotionCore-Queue", queue);
        this.AddHeader("Content-Type", "application/json");
        this.AddHeader("Accept", "*/*");
    }
}