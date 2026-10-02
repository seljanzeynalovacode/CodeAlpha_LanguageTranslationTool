namespace TranslationTool.Options;

public class MyMemoryOptions
{
    public const string SectionName = "MyMemory";

    public string BaseUrl { get; set; } = "https://api.mymemory.translated.net/";

    // İstəyə bağlı: e-poçt yazsan, gündəlik pulsuz limit artır
    public string? Email { get; set; }
}