namespace TranslationTool.WebAPI.Models
{
    public class TranslationResponse
    {
        public string TranslatedText { get; set; } = string.Empty;
        public string SourceLanguage { get; set; } = string.Empty;
        public string TargetLanguage { get; set; } = string.Empty;
    }
}