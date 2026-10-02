using System.Text;
using System.Text.Json;
using TranslationTool.Infrastructure;

namespace TranslationTool.WebAPI.Infrastructure;

public class GoogleTranslateProvider : ITranslationProvider
{
    private readonly HttpClient _httpClient;

    public GoogleTranslateProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
    }

    public async Task<string> TranslateAsync(string text, string sourceLanguage, string targetLanguage, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(text))
     return string.Empty;

 try
        {
     // LibreTranslate pulsuz API - daha etibarlı cümlə tərcüməsi üçün
     return await TranslateUsingLibreTranslate(text, sourceLanguage, targetLanguage, cancellationToken);
        }
        catch
        {
     // Fallback: MyMemory API-nə keçiş
     return await FallbackToMyMemory(text, sourceLanguage, targetLanguage, cancellationToken);
        }
    }

    private async Task<string> TranslateUsingLibreTranslate(string text, string source, string target, CancellationToken cancellationToken)
    {
        try
        {
     var url = "https://libretranslate.de/translate";

     var payload = new
     {
  q = text,
  source = MapLanguageCode(source),
         target = MapLanguageCode(target)
     };

     var json = JsonSerializer.Serialize(payload);
     var content = new StringContent(json, Encoding.UTF8, "application/json");

     var response = await _httpClient.PostAsync(url, content, cancellationToken);
            response.EnsureSuccessStatusCode();

     var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
     using var jsonDoc = JsonDocument.Parse(responseString);
     var root = jsonDoc.RootElement;

     if (root.TryGetProperty("translatedText", out var translatedText))
     {
         return translatedText.GetString() ?? text;
     }

     return text;
        }
        catch
 {
     throw;
        }
    }

    private async Task<string> FallbackToMyMemory(string text, string source, string target, CancellationToken cancellationToken)
    {
 try
        {
     var url = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(text)}&langpair={Uri.EscapeDataString($"{source}|{target}")}";
     var response = await _httpClient.GetAsync(url, cancellationToken);

     if (!response.IsSuccessStatusCode)
         return text;

     var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
     using var jsonDoc = JsonDocument.Parse(jsonString);
     var root = jsonDoc.RootElement;

     if (root.TryGetProperty("responseData", out var responseData) &&
         responseData.TryGetProperty("translatedText", out var translatedText))
     {
         return translatedText.GetString() ?? text;
     }

            return text;
 }
        catch
        {
            return text;
 }
    }

    private string MapLanguageCode(string code)
    {
        // LibreTranslate dəstəklədiyi dil kodları
        return code.ToLower() switch
        {
            "az" => "az",
     "en" => "en",
     "tr" => "tr",
     "ru" => "ru",
     "de" => "de",
     "fr" => "fr",
     "es" => "es",
            "pt" => "pt",
     "ja" => "ja",
     "zh" => "zh",
     "ar" => "ar",
     "hi" => "hi",
     _ => code.ToLower()
 };
    }
}