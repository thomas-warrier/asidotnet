namespace AspireApp1.ApiService.Features.Loueurs.Creer;

public record CreerLoueurRequest(
    string Nom, 
    string Prenom, 
    string Mobile, 
    string? Rue, 
    string? Cp, 
    string? Ville, 
    string? Pays
);