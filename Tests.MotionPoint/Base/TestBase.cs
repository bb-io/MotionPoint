using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Microsoft.Extensions.Configuration;

namespace Tests.MotionPoint.Base;

public class TestBase
{
    protected IEnumerable<AuthenticationCredentialsProvider> Credentials { get; set; }

    protected InvocationContext InvocationContext { get; set; }

    protected FileManagementClient FileManagementClient { get; set; }

    protected TestBase()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        Credentials = config.GetSection("ConnectionDefinition").GetChildren()
            .Select(x => new AuthenticationCredentialsProvider(x.Key, x.Value!)).ToList();
        
        InvocationContext = new InvocationContext
        {
            AuthenticationCredentialsProviders = Credentials,
        };
        
        FileManagementClient = new FileManagementClient();
    }
}
