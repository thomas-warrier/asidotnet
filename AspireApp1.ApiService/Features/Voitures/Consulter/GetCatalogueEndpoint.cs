using AspireApp1.ApiService.Data;
using AspireApp1.ApiService.Features.Voitures.GetCatalogue;
using Microsoft.EntityFrameworkCore;

namespace AspireApp1.ApiService.Features.Voitures.Consulter;

public static class GetCatalogueEndpoint
{
    public static void MapGetCatalogue(this IEndpointRouteBuilder app)
    {
        app.MapGet("/voiture-list", async (RentalDbContext db, string? categorie) =>
            {
                var todayUtc = DateTime.UtcNow.Date;

                var voitures = await db.Voitures
                    .Where(v => string.IsNullOrEmpty(categorie) || v.Categorie == categorie)
                    .ToListAsync();

                var result = voitures.Select(v => new VoitureDto(
                    v.Immatriculation,
                    v.Marque,
                    v.Modele,
                    v.Categorie,
                    v.PrixLocationParJour,
                    !db.Locations.Any(l => 
                        l.VoitureId == v.Id && 
                        !l.EstAnnule && 
                        l.DateDebut <= todayUtc && 
                        l.DateFin >= todayUtc)
                )).ToList();

                return Results.Ok(result);
            })
            .WithName("GetCatalogueVoitures")
            .WithTags("Voitures");
    }
}