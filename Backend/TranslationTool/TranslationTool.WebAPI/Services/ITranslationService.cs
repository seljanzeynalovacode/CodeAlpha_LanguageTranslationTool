using TranslationTool.Models;

namespace TranslationTool.Services;

public interface ITranslationService
{
    Task<string> TranslateAsync(
         string text,
         string sourceLanguage,
         string targetLanguage,
         CancellationToken cancellationToken = default);
}