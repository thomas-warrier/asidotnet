namespace AspireApp1.ApiService.Features.Voitures.GetCatalogue;

public record VoitureDto(
    string Immatriculation,
    string Marque,
    string Modele,
    string Categorie,
    decimal PrixParJour,
    bool EstDisponible
);