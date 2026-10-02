using TranslationTool.Infrastructure;
using TranslationTool.Services;

namespace TranslationTool.WebAPI.Services;

public class TranslationService : ITranslationService
{
    private readonly ITranslationProvider _translationProvider;

    public TranslationService(ITranslationProvider translationProvider)
    {
        _translationProvider = translationProvider ?? throw new ArgumentNullException(nameof(translationProvider));
    }

    public async Task<string> TranslateAsync(
        string text,
        string sourceLanguage,
        string targetLanguage,
        CancellationToken cancellationToken = default)
    {
        // 1. Mətnin boş olub-olmaması yoxlanılır
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        // 2. Dil kodlarının defolt dəyərlərinin təyini (əgər göndərilməyibsə)
        var source = string.IsNullOrWhiteSpace(sourceLanguage) ? "az" : sourceLanguage.Trim().ToLower();
        var target = string.IsNullOrWhiteSpace(targetLanguage) ? "en" : targetLanguage.Trim().ToLower();

        // 3. Eyni dilə tərcümə edilmək istənirsə, sorğu göndərmədən mətni olduğu kimi qaytarır
        if (source.Equals(target, StringComparison.OrdinalIgnoreCase))
        {
            return text;
        }

        // 4. Əsas tərcümə provayderinə çağırış olunur
        var translatedText = await _translationProvider.TranslateAsync(
            text,
            source,
            target,
            cancellationToken);

        return translatedText;
    }
}