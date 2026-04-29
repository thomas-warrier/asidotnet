using AspireApp1.ApiService.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspireApp1.ApiService.Data;

public class RentalDbContext(DbContextOptions<RentalDbContext> options) :
    DbContext(options)
{
    public DbSet<Loueur> Loueurs => Set<Loueur>();
    public DbSet<Voiture> Voitures => Set<Voiture>();
    public DbSet<Location> Locations => Set<Location>();
}