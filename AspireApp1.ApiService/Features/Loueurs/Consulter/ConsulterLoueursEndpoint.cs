using AspireApp1.ApiService.Data;
using Microsoft.EntityFrameworkCore;

namespace AspireApp1.ApiService.Features.Loueurs.Consulter;

public static class ConsulterLoueursEndpoints
{
    public static void MapConsulterLoueurs(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/loueurs").WithTags("Loueurs");

        group.MapGet("/", async (RentalDbContext db) => 
                await db.Loueurs.ToListAsync())
            .WithName("GetTousLoueurs");

        group.MapGet("/{id:int}", async (int id, RentalDbContext db) => 
                await db.Loueurs.FindAsync(id) is { } loueur 
                    ? Results.Ok(loueur) 
                    : Results.NotFound("Loueur introuvable."))
            .WithName("GetLoueurById");
    }
}