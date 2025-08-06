using Apps.MotionPoint.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.MotionPoint;

public class Invocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Credentials =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected Client Client { get; }

    protected Invocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Credentials.ToList());
    }
}
