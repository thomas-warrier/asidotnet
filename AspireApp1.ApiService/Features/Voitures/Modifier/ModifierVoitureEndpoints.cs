using AspireApp1.ApiService.Data;
using AspireApp1.ApiService.Domain;

namespace AspireApp1.ApiService.Features.Voitures.Modifier;

public static class ModifierVoitureEndpoints
{
    public static void MapModifierVoitures(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/voitures").WithTags("Voitures");

        group.MapPut("/{id:int}", async (int id, Voiture input, RentalDbContext db) => 
            {
                var v = await db.Voitures.FindAsync(id);
                if (v == null) return Results.NotFound();
            
                v.Marque = input.Marque; 
                v.Modele = input.Modele; 
                v.Categorie = input.Categorie;
                v.Immatriculation = input.Immatriculation;
            
                await db.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("ModifierVoitureComplete");

        group.MapPatch("/{id:int}/prix", async (int id, decimal nouveauPrix, RentalDbContext db) => 
            {
                var v = await db.Voitures.FindAsync(id);
                if (v == null) return Results.NotFound();
            
                v.PrixLocationParJour = nouveauPrix;
                await db.SaveChangesAsync();
                return Results.Ok(v);
            })
            .WithName("ModifierPrixVoiture");
    }
}