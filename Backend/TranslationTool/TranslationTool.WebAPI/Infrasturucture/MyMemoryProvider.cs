using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Transactions;
using TranslationTool.Options;
using TranslationTool.Services;

namespace TranslationTool.Infrastructure;

public class MyMemoryProvider : ITranslationProvider
{
    private readonly HttpClient _http;
    private readonly MyMemoryOptions _options;

    public MyMemoryProvider(HttpClient http, IOptions<MyMemoryOptions> options)
    {
        _http = http;
        _options = options.Value;
    }

    public async Task<string> TranslateAsync(string text, string source, string target, CancellationToken ct)
    {
        var url = $"get?q={Uri.EscapeDataString(text)}&langpair={Uri.EscapeDataString($"{source}|{target}")}";

        if (!string.IsNullOrWhiteSpace(_options.Email))
            url += $"&de={Uri.EscapeDataString(_options.Email)}";

        HttpResponseMessage response;
        try
        {
            response = await _http.GetAsync(url, ct);
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        {
            throw new TranslationException("Tərcümə xidmətinə qoşulmaq mümkün olmadı.", 502);
        }

        if (!response.IsSuccessStatusCode)
            throw new TranslationException("Tərcümə xidməti xəta qaytardı.", 502);

        var json = await response.Content.ReadAsStringAsync(ct);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var status = root.TryGetProperty("responseStatus", out var s) ? s.ToString() : "";
        if (status != "200")
        {
            var details = root.TryGetProperty("responseDetails", out var d) ? d.GetString() : null;
            throw new TranslationException(details ?? "Tərcümə alınmadı (limit bitmiş ola bilər).", 502);
        }

        return root.GetProperty("responseData").GetProperty("translatedText").GetString() ?? string.Empty;
    }
}