using AspireApp1.ApiService.Data;
using AspireApp1.ApiService.Features.Locations.Annuler;
using AspireApp1.ApiService.Features.Locations.Consulter;
using AspireApp1.ApiService.Features.Locations.Reserver;
using AspireApp1.ApiService.Features.Locations.Service;
using AspireApp1.ApiService.Features.Loueurs.Consulter;
using AspireApp1.ApiService.Features.Loueurs.Creer;
using AspireApp1.ApiService.Features.Loueurs.Modifier;
using AspireApp1.ApiService.Features.Voitures.Consulter;
using AspireApp1.ApiService.Features.Voitures.Creer;
using AspireApp1.ApiService.Features.Voitures.GetCatalogue;
using AspireApp1.ApiService.Features.Voitures.Modifier;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<LocationService>();
builder.AddNpgsqlDbContext<RentalDbContext>("RentalDb");
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Location Auto API")
            .WithTheme(ScalarTheme.Moon) // Choix du thème
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

string[] summaries =
    ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/", () => "API service is running. Navigate to /weatherforecast to see sample data.");

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.MapGetCatalogue();
app.MapGetVoitures();
app.MapCreerVoiture();
app.MapModifierVoitures();

app.MapReserver();
app.MapAnnulerLocation();
app.MapLocationQueries();

app.MapCreerLoueur();
app.MapConsulterLoueurs();
app.MapModifierLoueurs();

app.MapDefaultEndpoints();
// --- Section Migration Automatique avec Retry ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<RentalDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    for (int i = 0; i < 5; i++) 
    {
        try
        {
            logger.LogInformation("Tentative de connexion à PostgreSQL... (Essai {res})", i + 1);
            await context.Database.MigrateAsync();
            await SeedData.Initialize(context);
            logger.LogInformation("BD prête et remplie !");
            break; 
        }
        catch (Exception ex)
        {
            logger.LogWarning("La base n'est pas prête, nouvelle tentative dans 2s...");
            await Task.Delay(2000);
            if (i == 4) logger.LogError(ex, "Erreur finale après 5 tentatives.");
        }
    }
}
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}