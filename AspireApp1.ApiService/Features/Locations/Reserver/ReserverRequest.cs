namespace AspireApp1.ApiService.Features.Locations.Reserver;


public record ReserverRequest(
    string Immatriculation, 
    int LoueurId, 
    DateTime DateDebut, 
    DateTime DateFin
);