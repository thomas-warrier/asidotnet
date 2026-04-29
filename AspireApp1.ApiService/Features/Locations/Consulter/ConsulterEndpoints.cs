using AspireApp1.ApiService.Data;
using Microsoft.EntityFrameworkCore;

namespace AspireApp1.ApiService.Features.Locations.Consulter;

public static class ConsulterEndpoints
{
    public static void MapLocationQueries(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/locations").WithTags("Locations");

        // Obtenir toutes les locations
        group.MapGet("/", async (RentalDbContext db) => 
                await db.Locations.ToListAsync())
            .WithName("GetToutesLocations");

        // Obtenir les locations par immatriculation
        group.MapGet("/voiture/{immat}", async (string immat, RentalDbContext db) => 
            {
                return await db.Locations
                    .Include(l => l.Voiture)
                    .Where(l => l.Voiture.Immatriculation == immat)
                    .ToListAsync();
            })
            .WithName("GetLocationsParImmat");
    }
}