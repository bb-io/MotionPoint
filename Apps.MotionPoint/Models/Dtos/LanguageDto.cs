namespace Apps.MotionPoint.Models.Dtos;

public class LanguageDto
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public CountryDto? Country { get; set; }
}