using Microsoft.AspNetCore.Mvc;
using TranslationTool.Models;
using TranslationTool.Services;
using TranslationTool.WebAPI.Models;

namespace TranslationTool.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TranslationController : ControllerBase
{
    private readonly ITranslationService _translationService;

    public TranslationController(ITranslationService translationService)
    {
        _translationService = translationService;
    }

    [HttpPost]
    public async Task<IActionResult> Translate([FromBody] TranslationRequest request, CancellationToken cancellationToken)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest(new { error = "Mətn daxil edilməlidir." });
        }

        var translatedText = await _translationService.TranslateAsync(
            request.Text,
            request.SourceLanguage,
            request.TargetLanguage,
            cancellationToken);

        var response = new TranslationResponse
        {
            TranslatedText = translatedText,
            SourceLanguage = request.SourceLanguage,
            TargetLanguage = request.TargetLanguage
        };

        return Ok(response);
    }
}