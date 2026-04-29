namespace AspireApp1.ApiService.Features.Locations.Reserver;

public record ReserverResponse(
    int LocationId, 
    decimal PrixTotal, 
    string Message
);