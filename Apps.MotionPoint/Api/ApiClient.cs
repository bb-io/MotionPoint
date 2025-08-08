using Apps.MotionPoint.Constants;
using Apps.MotionPoint.Models.Dtos;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.MotionPoint.Api;

public class ApiClient : BlackBirdRestClient
{
    public ApiClient(List<AuthenticationCredentialsProvider> credentialsProviders) : base(new()
    {
        BaseUrl = BuildUrl(credentialsProviders: credentialsProviders),
        ThrowOnAnyError = false
    })
    {
        this.AddDefaultHeader("Authorization", credentialsProviders.Get(CredNames.ApiKey).Value);
        this.AddDefaultHeader("X-MotionCore-UserName", credentialsProviders.Get(CredNames.Username).Value);
        this.AddDefaultHeader("X-MotionCore-ApiId", "1");
    }

    public async Task<List<T>> PaginateAsync<T>(RestRequest request)
    {
        var allResults = new List<T>();
        const int defaultPageSize = 25;
        
        if (request.Parameters.All(p => p.Name != "size"))
        {
            request.AddQueryParameter("size", defaultPageSize.ToString());
        }
        
        var currentPage = 0;
        bool isLastPage;
        do
        {
            var existingPageParam = request.Parameters.FirstOrDefault(p => p.Name == "page" && p.Type == ParameterType.QueryString);
            if (existingPageParam != null)
            {
                request.RemoveParameter(existingPageParam);
            }

            request.AddQueryParameter("page", currentPage.ToString());
            var paginatedResponse = await ExecuteWithErrorHandling<PaginationDto<T>>(request);
            if (paginatedResponse.Content.Any())
            {
                allResults.AddRange(paginatedResponse.Content);
            }
            
            isLastPage = paginatedResponse.Last;
            currentPage++;
            
        } while (!isLastPage);
        
        return allResults;
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        if (string.IsNullOrEmpty(response.Content))
        {
            if (string.IsNullOrEmpty(response.ErrorMessage))
            {
                return new PluginApplicationException($"Status code: {response.StatusCode}, but no content or error message was provided.");
            }

            return new PluginApplicationException($"{response.ErrorMessage} ({response.StatusCode})");
        }

        if (response.Content.Contains("MotionPoint Sandbox Application is Bootstrapping!"))
        {
            return new PluginMisconfigurationException("MotionPoint Sandbox Application is Bootstrapping! Please retry in a few minutes.");
        }

        if (response.ContentType == "application/json")
        {
            var errorDto = JsonConvert.DeserializeObject<ErrorDto>(response.Content!);
            if (errorDto != null)
            {
                return new PluginApplicationException(errorDto.ToString());
            }
        }

        return new PluginApplicationException($"{response.StatusCode}. Content: {response.Content}");
    }
    
    private static Uri BuildUrl(List<AuthenticationCredentialsProvider> credentialsProviders)
    {
        var environmentProvider = credentialsProviders.Get(CredNames.Environment);
        if (string.IsNullOrEmpty(environmentProvider.Value))
        {
            throw new Exception("Could not find environment credential provider");
        }
        
        var stringUrl = $"https://{environmentProvider.Value}";
        return new(stringUrl);
    }
}