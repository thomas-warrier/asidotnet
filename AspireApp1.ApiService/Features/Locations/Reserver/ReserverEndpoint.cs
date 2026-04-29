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
        // AJOUT : Injection de IHttpClientFactory pour pouvoir appeler le service de paiement
        app.MapPost("/locations", async (ReserverRequest req, RentalDbContext db, LocationService service, IHttpClientFactory clientFactory) =>
            {
                // 1. Verif loueur et blacklist
                var loueur = await db.Loueurs.FindAsync(req.LoueurId);
            
                if (loueur == null) 
                    return Results.NotFound("Le loueur spécifié n'existe pas.");
                
                if (loueur.EstBlackliste) 
                    return Results.Problem(
                        title: "Action refusée",
                        detail: "Ce loueur est sur liste noire et ne peut pas effectuer de réservation.", 
                        statusCode: StatusCodes.Status403Forbidden // Retourne une erreur 403 !
                    );

                // 2. Verif voiture
                var voiture = await db.Voitures.FirstOrDefaultAsync(v => v.Immatriculation == req.Immatriculation);
                if (voiture == null) return Results.NotFound("Voiture non trouvée.");
            
                // 3. Calcul du prix
                var prix = service.CalculerPrixTotal(voiture.PrixLocationParJour, req.DateDebut, req.DateFin);

                // 4. NOUVEAU : Appel au service de paiement (PaiementService)
                var httpClient = clientFactory.CreateClient("PaiementClient");
                var paiementData = new { NumeroCarte = req.NumeroCarte, DateExpiration = req.DateExpiration, Montant = prix };
                
                var reponsePaiement = await httpClient.PostAsJsonAsync("/payer", paiementData);
                
                if (!reponsePaiement.IsSuccessStatusCode)
                {
                    // Si l'algorithme de Luhn ou la date échoue, on bloque la réservation
                    return Results.BadRequest("Le paiement a été refusé. Réservation annulée.");
                }

                // 5. Création de la location
                var nouvelleLocation = new Location
                {
                    VoitureId = voiture.Id,
                    LoueurId = req.LoueurId,
                    DateDebut = DateTime.SpecifyKind(req.DateDebut, DateTimeKind.Utc),
                    DateFin = DateTime.SpecifyKind(req.DateFin, DateTimeKind.Utc),
                    EstAnnule = false,
                    EstPaye = true // 👈 Le paiement a été validé !
                };

                db.Locations.Add(nouvelleLocation);
                await db.SaveChangesAsync();

                return Results.Created($"/locations/{nouvelleLocation.Id}", 
                    new ReserverResponse(nouvelleLocation.Id, prix, "Réservation confirmée et payée !"));
            })
            .WithName("CreerLocation")
            .WithTags("Locations")
            .AddEndpointFilter<ValidationFilter<ReserverRequest>>();
    }
}