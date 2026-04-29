using AspireApp1.ApiService.Data;
using AspireApp1.ApiService.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspireApp1.ApiService.Features.Voitures.Consulter;

public static class GetVoitureEndpoints
{
    public static void MapGetVoitures(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/voitures").WithTags("Voitures");

        group.MapGet("/{id:int}", async (int id, RentalDbContext db) =>
                await db.Voitures.FindAsync(id) is Voiture v ? Results.Ok(v) : Results.NotFound())
            .WithName("GetVoitureById");

        group.MapGet("/by-immat/{immat}", async (string immat, RentalDbContext db) =>
                await db.Voitures.FirstOrDefaultAsync(v => v.Immatriculation == immat) is Voiture v 
                    ? Results.Ok(v) : Results.NotFound())
            .WithName("GetVoitureByImmat");
    }
}