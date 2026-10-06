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

// CORS Politikası: Test üçün tam açıq vəziyyətə gətirildi (AllowAll)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // İstənilən portdan (5500, 5501, 3000 və s.) gələn sorğuya icazə verir
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

// Yaratdığımız AllowAll siyasətini tətbiq edirik
app.UseCors("AllowAll");

app.MapControllers();

app.Run();