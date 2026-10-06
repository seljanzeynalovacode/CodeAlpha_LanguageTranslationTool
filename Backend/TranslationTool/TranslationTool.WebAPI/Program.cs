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


builder.Services.AddScoped<ITranslationService, TranslationService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   
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