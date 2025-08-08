using Apps.MotionPoint.Models.Dtos;
using Blackbird.Applications.Sdk.Common;

namespace Apps.MotionPoint.Models.Responses;

public class FullJobResponse : JobResponse
{
    [Display("Words translated")]
    public long WordsTranslated { get; set; }

    [Display("Words not translate")]
    public long WordsNotTranslated { get; set; }

    [Display("Percentage text translated")]
    public double PercentageTextTranslated { get; set; }

    [Display("Files translated")]
    public long FilesTranslated { get; set; }

    [Display("Files not translated")]
    public long FilesNotTranslated { get; set; }

    [Display("Percentage files translated")]
    public double PercentageFilesTranslated { get; set; }

    public FullJobResponse(JobResponse jobResponse, TranslationStatisticsDetailDto translationStatisticsDetailDto)
    {
        Id = jobResponse.Id;
        Status = jobResponse.Status;
        SourceLanguage = jobResponse.SourceLanguage;
        TargetLanguage = jobResponse.TargetLanguage;
        TargetCountry = jobResponse.TargetCountry;
        CustomerId = jobResponse.CustomerId;
        ReceiptDate = jobResponse.ReceiptDate;
        CompletionDate = jobResponse.CompletionDate;
        SubmittedBy = jobResponse.SubmittedBy;
        TransactionReferenceId = jobResponse.TransactionReferenceId;
        WordsTranslated = translationStatisticsDetailDto.WordsTranslated;
        WordsNotTranslated = translationStatisticsDetailDto.WordsNotTranslated;
        PercentageTextTranslated = translationStatisticsDetailDto.PercentageTextTranslated;
        FilesTranslated = translationStatisticsDetailDto.FilesTranslated;
        FilesNotTranslated = translationStatisticsDetailDto.FilesNotTranslated;
        PercentageFilesTranslated = translationStatisticsDetailDto.PercentageFilesTranslated;
    }
}