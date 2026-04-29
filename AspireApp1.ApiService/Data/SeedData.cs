using AspireApp1.ApiService.Data;
using AspireApp1.ApiService.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspireApp1.ApiService.Data;

public static class SeedData
{
    public static async Task Initialize(RentalDbContext context)
    {
        // --- 1. SEED DES VOITURES ---
        if (!await context.Voitures.AnyAsync())
        {
            await context.Voitures.AddRangeAsync(
                new Voiture
                {
                    Immatriculation = "AA-123-BB",
                    Marque = "Tesla",
                    Modele = "Model 3",
                    Categorie = "Électrique",
                    PrixLocationParJour = 120.00m
                },
                new Voiture
                {
                    Immatriculation = "BB-456-CC",
                    Marque = "Peugeot",
                    Modele = "208",
                    Categorie = "Citadine",
                    PrixLocationParJour = 45.50m
                },
                new Voiture
                {
                    Immatriculation = "CC-789-DD",
                    Marque = "Volkswagen",
                    Modele = "Golf 8",
                    Categorie = "Compacte",
                    PrixLocationParJour = 85.00m
                },
                new Voiture
                {
                    Immatriculation = "DD-000-EE",
                    Marque = "BMW",
                    Modele = "Série 3",
                    Categorie = "Berline",
                    PrixLocationParJour = 150.00m
                }
            );
            
            // On sauvegarde les voitures avant de passer à la suite
            await context.SaveChangesAsync();
        }

        // --- 2. SEED DES LOUEURS (CLIENTS) ---
        // Indispensable pour éviter l'erreur de Foreign Key (FK) lors d'une réservation
        if (!await context.Loueurs.AnyAsync())
        {
            await context.Loueurs.AddAsync(new Loueur
            {
                // L'Id sera généré automatiquement (généralement 1)
                Nom = "Dupont",
                Prenom = "Jean",
                Mobile = "0601020304"
            });

            await context.SaveChangesAsync();
        }
        
        Console.WriteLine("Données de test (Voitures et Loueurs) injectées avec succès !");
    }
}