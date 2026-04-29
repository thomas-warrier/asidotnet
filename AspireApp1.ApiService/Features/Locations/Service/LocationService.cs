namespace AspireApp1.ApiService.Features.Locations.Service;

public class LocationService
{
    public decimal CalculerPrixTotal(decimal prixJour, DateTime debut, DateTime fin)
    {
        if (fin.Date < debut.Date)
            throw new ArgumentException("La date de fin ne peut pas être antérieure à la date de début.");

        int nombreDeJours = (fin.Date - debut.Date).Days + 1;
        
        return prixJour * nombreDeJours;
    }
}