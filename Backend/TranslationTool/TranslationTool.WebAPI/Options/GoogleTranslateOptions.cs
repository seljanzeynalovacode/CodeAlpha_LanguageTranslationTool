namespace TranslationTool.WebAPI.Options;

public class GoogleTranslateOptions
{
    public const string SectionName = "GoogleTranslateOptions";

    public string ApiKey { get; set; } = string.Empty;
    public string ApiHost { get; set; } = "google-translate113.p.rapidapi.com";
}