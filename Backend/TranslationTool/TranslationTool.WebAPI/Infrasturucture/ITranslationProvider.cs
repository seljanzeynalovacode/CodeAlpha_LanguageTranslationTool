namespace TranslationTool.Infrastructure;

public interface ITranslationProvider
{
    Task<string> TranslateAsync(string text, string source, string target, CancellationToken ct);
}