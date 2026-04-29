public record ReserverRequest(
    string Immatriculation, 
    int LoueurId, 
    DateTime DateDebut, 
    DateTime DateFin,
    string NumeroCarte, 
    string DateExpiration 
);