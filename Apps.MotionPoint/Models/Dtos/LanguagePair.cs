namespace Apps.MotionPoint.Models.Dtos;

public class LanguagePair
{
    public LanguageDto SourceLanguage { get; set; } = new();
    public LanguageDto TargetLanguage { get; set; } = new();
    public string Queue { get; set; } = string.Empty;
}