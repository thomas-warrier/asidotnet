using AspireApp1.ApiService.Data;
using AspireApp1.ApiService.Domain;

namespace AspireApp1.ApiService.Features.Voitures.Creer;

public static class CreerVoitureEndpoint
{
    public static void MapCreerVoiture(this IEndpointRouteBuilder app)
    {
        app.MapPost("/voitures", async (Voiture voiture, RentalDbContext db) => 
            {
                db.Voitures.Add(voiture);
                await db.SaveChangesAsync();
                return Results.Created($"/voitures/{voiture.Id}", voiture);
            })
            .WithName("CreerVoiture")
            .WithTags("Voitures");
    }
}