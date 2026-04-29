using AspireApp1.ApiService.Data;
using AspireApp1.ApiService.Domain;
using AspireApp1.ApiService.Features.Locations.Service;
using AspireApp1.ApiService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AspireApp1.ApiService.Features.Locations.Reserver;

public static class ReserverEndpoint
{
    public static void MapReserver(this IEndpointRouteBuilder app)
    {
        app.MapPost("/locations", async (ReserverRequest req, RentalDbContext db, LocationService service) =>
            {
                var voiture = await db.Voitures.FirstOrDefaultAsync(v => v.Immatriculation == req.Immatriculation);
                if (voiture == null) return Results.NotFound("Voiture non trouvée.");
            
                var prix = service.CalculerPrixTotal(voiture.PrixLocationParJour, req.DateDebut, req.DateFin);

                var nouvelleLocation = new Location
                {
                    VoitureId = voiture.Id,
                    LoueurId = req.LoueurId,
                    DateDebut = DateTime.SpecifyKind(req.DateDebut, DateTimeKind.Utc),
                    DateFin = DateTime.SpecifyKind(req.DateFin, DateTimeKind.Utc),
                    EstAnnule = false
                };

                db.Locations.Add(nouvelleLocation);
                await db.SaveChangesAsync();

                return Results.Created($"/locations/{nouvelleLocation.Id}", 
                    new ReserverResponse(nouvelleLocation.Id, prix, "Réservation confirmée"));
            })
            .WithName("CreerLocation")
            .WithTags("Locations")
            .AddEndpointFilter<ValidationFilter<ReserverRequest>>();
    }
}