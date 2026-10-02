using System.ComponentModel.DataAnnotations;

namespace TranslationTool.Models;

public class TranslationRequest
{
    [Required, StringLength(500, MinimumLength = 1)]
    public string Text { get; set; } = string.Empty;

    [Required]
    public string SourceLanguage { get; set; } = string.Empty;

    [Required]
    public string TargetLanguage { get; set; } = string.Empty;
}