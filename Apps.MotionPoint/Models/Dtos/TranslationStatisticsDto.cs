using Newtonsoft.Json;

namespace Apps.MotionPoint.Models.Dtos;

public class TranslationStatisticsDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("translationJobPageStatistics")]
    public List<TranslationJobPageStatisticsDto> TranslationJobPageStatistics { get; set; } = new();

    [JsonProperty("translationStatistics")]
    public TranslationStatisticsDetailDto TranslationStatistics { get; set; } = new();

    [JsonProperty("completionDate")]
    public DateTime CompletionDate { get; set; }
}

public class TranslationJobPageStatisticsDto
{
    [JsonProperty("translationJobPage")]
    public TranslationJobPageDto TranslationJobPage { get; set; } = new();
}

public class TranslationJobPageDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("pageUrl")]
    public string PageUrl { get; set; } = string.Empty;

    [JsonProperty("queueDate")]
    public DateTime QueueDate { get; set; }
}

public class TranslationStatisticsDetailDto
{
    [JsonProperty("wordsTranslated")]
    public int WordsTranslated { get; set; }

    [JsonProperty("wordsNotTranslated")]
    public int WordsNotTranslated { get; set; }

    [JsonProperty("wordsSuppressed")]
    public int WordsSuppressed { get; set; }

    [JsonProperty("percentageTextTranslated")]
    public double PercentageTextTranslated { get; set; }

    [JsonProperty("filesTranslated")]
    public int FilesTranslated { get; set; }

    [JsonProperty("filesNotTranslated")]
    public int FilesNotTranslated { get; set; }

    [JsonProperty("percentageFilesTranslated")]
    public double PercentageFilesTranslated { get; set; }
}