using Apps.MotionPoint.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.MotionPoint.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        new()
        {
            Name = "Developer API key",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionProperties = new List<ConnectionProperty>
            {
                new(CredNames.Environment)
                {
                    DisplayName = "Environment",
                    DataItems = 
                    [
                        new("sandboxapi.motionpoint.com", "Sandbox (sandboxapi.motionpoint.com)"),
                        new("api.motionpoint.com", "Production (api.motionpoint.com")
                    ]
                },
                new(CredNames.Username)
                {
                    DisplayName = "Username",
                    Description = "Your username for accessing the MotionPoint Developer API."
                },
                new(CredNames.ApiKey)
                {
                    DisplayName = "API Key",
                    Sensitive = true,
                    Description = "Your API key for accessing the MotionPoint Developer API."
                }
            }
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(Dictionary<string, string> values) => 
        values.Select(x => new AuthenticationCredentialsProvider(x.Key, x.Value)).ToList();
}
