using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.MotionPoint.Models.Dtos;

public class TranslationStatisticsDto
{
    [Display("Job ID")]
    [JsonProperty("id")]
    public int Id { get; set; }

    [Display("Job status")]
    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [Display("Translation job page statistics")]
    [JsonProperty("translationJobPageStatistics")]
    public List<TranslationJobPageStatisticsDto> TranslationJobPageStatistics { get; set; } = new();

    [Display("Translation statistics")]
    [JsonProperty("translationStatistics")]
    public TranslationStatisticsDetailDto TranslationStatistics { get; set; } = new();

    [Display("Completion date")]
    [JsonProperty("completionDate")]
    public DateTime CompletionDate { get; set; }
}

public class TranslationJobPageStatisticsDto
{
    [Display("Translation job page")]
    [JsonProperty("translationJobPage")]
    public TranslationJobPageDto TranslationJobPage { get; set; } = new();
}

public class TranslationJobPageDto
{
    [Display("Page ID")]
    [JsonProperty("id")]
    public int Id { get; set; }

    [Display("Page URL")]
    [JsonProperty("pageUrl")]
    public string PageUrl { get; set; } = string.Empty;

    [Display("Queue date")]
    [JsonProperty("queueDate")]
    public DateTime QueueDate { get; set; }
}

public class TranslationStatisticsDetailDto
{
    [Display("Words translated")]
    [JsonProperty("wordsTranslated")]
    public int WordsTranslated { get; set; }

    [Display("Words not translated")]
    [JsonProperty("wordsNotTranslated")]
    public int WordsNotTranslated { get; set; }

    [Display("Words suppressed")]
    [JsonProperty("wordsSuppressed")]
    public int WordsSuppressed { get; set; }

    [Display("Percentage text translated")]
    [JsonProperty("percentageTextTranslated")]
    public double PercentageTextTranslated { get; set; }

    [Display("Files translated")]
    [JsonProperty("filesTranslated")]
    public int FilesTranslated { get; set; }

    [Display("Files not translated")]
    [JsonProperty("filesNotTranslated")]
    public int FilesNotTranslated { get; set; }

    [Display("Percentage files translated")]
    [JsonProperty("percentageFilesTranslated")]
    public double PercentageFilesTranslated { get; set; }
}