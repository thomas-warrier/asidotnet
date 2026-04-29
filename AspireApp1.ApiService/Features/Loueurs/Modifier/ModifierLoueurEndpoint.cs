using AspireApp1.ApiService.Data;

namespace AspireApp1.ApiService.Features.Loueurs.Modifier;

// 1. On crée un mini-DTO dédié à cette action
public record UpdateBlacklistRequest(bool EstBlackliste);

public static class ModifierLoueurEndpoints
{
    public static void MapModifierLoueurs(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/loueurs").WithTags("Loueurs");

        // 2. On remplace "bool blackliste" par "UpdateBlacklistRequest req"
        group.MapPatch("/{id:int}/blacklist", async (int id, UpdateBlacklistRequest req, RentalDbContext db) =>
            {
                var loueur = await db.Loueurs.FindAsync(id);
                if (loueur == null) return Results.NotFound("Loueur introuvable.");

                // 3. On utilise la valeur du DTO
                loueur.EstBlackliste = req.EstBlackliste;
                await db.SaveChangesAsync();

                var statut = req.EstBlackliste ? "ajouté à la liste noire" : "retiré de la liste noire";
                return Results.Ok(new { message = $"Le loueur {loueur.Nom} a été {statut}." });
            })
            .WithName("BlacklisterLoueur");
    }
}