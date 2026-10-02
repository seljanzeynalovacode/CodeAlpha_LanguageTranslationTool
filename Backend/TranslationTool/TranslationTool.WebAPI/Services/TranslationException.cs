namespace TranslationTool.Services;

public class TranslationException : Exception
{
    public int StatusCode { get; }

    public TranslationException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}