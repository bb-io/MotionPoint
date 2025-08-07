namespace Apps.MotionPoint.Models.Dtos;

public class LanguagePairsConfiguration
{
    public int ApiId { get; set; }
    public List<LanguagePair> LocaleData { get; set; } = new();
}