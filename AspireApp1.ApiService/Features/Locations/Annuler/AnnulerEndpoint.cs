using AspireApp1.ApiService.Data;
using Microsoft.EntityFrameworkCore;

namespace AspireApp1.ApiService.Features.Locations.Annuler;

public static class AnnulerEndpoint
{
    public static void MapAnnulerLocation(this IEndpointRouteBuilder app)
    {
        // On peut créer un groupe même pour une seule route pour garder les tags uniformes
        app.MapPatch("/locations/{id:int}/annuler", async (int id, RentalDbContext db) => 
            {
                var loc = await db.Locations.FindAsync(id);
                if (loc == null) return Results.NotFound();
            
                loc.EstAnnule = true; 
            
                await db.SaveChangesAsync();
                return Results.Ok(new { message = "Location annulée", id });
            })
            .WithName("AnnulerLocation")
            .WithTags("Locations");
    }
}