using Microsoft.Extensions.Options;
using TranslationTool.Infrastructure;
using TranslationTool.Middleware;
using TranslationTool.Services;
using TranslationTool.WebAPI.Infrastructure;
using TranslationTool.WebAPI.Options;
using TranslationTool.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpClient və GoogleTranslateProvider Registrasiyası
builder.Services.AddHttpClient<GoogleTranslateProvider>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddScoped<ITranslationProvider>(sp => sp.GetRequiredService<GoogleTranslateProvider>());

// Service Qatı Registrasiyası
builder.Services.AddScoped<ITranslationService, TranslationService>();

// CORS Politikası
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
       .AllowAnyHeader()
.AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("Frontend");
app.MapControllers();

app.Run();