using AspireApp1.ApiService.Data;
using AspireApp1.ApiService.Domain;
using AspireApp1.ApiService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AspireApp1.ApiService.Features.Loueurs.Creer;

public static class CreerLoueurEndpoint
{
    public static void MapCreerLoueur(this IEndpointRouteBuilder app)
    {
        app.MapPost("/loueurs", async (CreerLoueurRequest req, RentalDbContext db) =>
            {
                // Vérification de l'unicité du mobile (car tu as mis un Index IsUnique = true)
                var mobileExiste = await db.Loueurs.AnyAsync(l => l.Mobile == req.Mobile);
                if (mobileExiste)
                {
                    return Results.Conflict(new { message = "Un loueur avec ce numéro de mobile existe déjà." });
                }

                var nouveauLoueur = new Loueur
                {
                    Nom = req.Nom,
                    Prenom = req.Prenom,
                    Mobile = req.Mobile,
                    Rue = req.Rue,
                    Cp = req.Cp,
                    Ville = req.Ville,
                    // Si le pays n'est pas précisé dans la requête, on force "France"
                    Pays = string.IsNullOrWhiteSpace(req.Pays) ? "France" : req.Pays,
                    EstBlackliste = false 
                };

                db.Loueurs.Add(nouveauLoueur);
                await db.SaveChangesAsync();

                return Results.Created($"/loueurs/{nouveauLoueur.Id}", nouveauLoueur);
            })
            .WithName("CreerLoueur")
            .WithTags("Loueurs")
            .AddEndpointFilter<ValidationFilter<CreerLoueurRequest>>();
    }
}