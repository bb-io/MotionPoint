using Apps.MotionPoint.Api;
using Apps.MotionPoint.Models.Dtos;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.MotionPoint.Services;

public class LanguageMappingService(InvocationContext invocationContext)
{
    private readonly Client _client = new(invocationContext.AuthenticationCredentialsProviders.ToList());
    public async Task<string> GetQueueIdentifierAsync(string sourceLanguage, string targetLanguage, string? country)
    {
        var apiRequest = new RestRequest("/languages");
        var response = await _client.ExecuteWithErrorHandling<LanguagePairsConfiguration>(apiRequest);
        
        var languagePair = response.LocaleData.FirstOrDefault(x =>
            x.SourceLanguage.Code == sourceLanguage &&
            x.TargetLanguage.Code == targetLanguage &&
            (string.IsNullOrEmpty(country) || x.TargetLanguage.Country?.Code == country));

        if (languagePair == null)
        {
            var availableQueues = response.LocaleData
                .GroupBy(x => $"{x.SourceLanguage.Code} -> {x.TargetLanguage.Code}")
                .Select(group => 
                {
                    var queuesInGroup = group.Select(x => 
                        x.TargetLanguage.Country != null 
                            ? $"{x.Queue} (country: {x.TargetLanguage.Country.Code})"
                            : x.Queue);
                    return $"{group.Key}: {string.Join(", ", queuesInGroup)}";
                })
                .ToList();

            var availableQueuesString = string.Join(";  ", availableQueues);
            var searchCriteria = string.IsNullOrEmpty(country) 
                ? $"source language '{sourceLanguage}' and target language '{targetLanguage}'"
                : $"source language '{sourceLanguage}', target language '{targetLanguage}', and country '{country}'";

            throw new PluginApplicationException($"No queue found for {searchCriteria}; Available language pairs and queues:  {availableQueuesString}");
        }

        return languagePair.Queue;
    }
}