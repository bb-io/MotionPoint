using Apps.MotionPoint.Api;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;

namespace Apps.MotionPoint.Connections;

public class ConnectionValidator: IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders, 
        CancellationToken cancellationToken)
    {
        try
        {
            var client = new Client(authenticationCredentialsProviders.ToList());
            var request = new RestRequest("/languages");
            
            var response = await client.ExecuteWithErrorHandling(request);
            return new()
            {
                IsValid = response.IsSuccessStatusCode
            };
        } 
        catch (Exception ex)
        {
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }

    }
}
