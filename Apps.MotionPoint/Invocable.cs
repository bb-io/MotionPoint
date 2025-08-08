using Apps.MotionPoint.Api;
using Apps.MotionPoint.Models.Dtos;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.MotionPoint;

public class Invocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Credentials =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected ApiClient Client { get; }

    protected Invocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Credentials.ToList());
    }
}
