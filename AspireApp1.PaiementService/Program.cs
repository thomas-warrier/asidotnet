var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceDiscovery();
var app = builder.Build();

app.MapPost("/payer", (PaiementRequest req) =>
{
    // 1. Vérification de la date d'expiration (doit être >= mois en cours)
    var parts = req.DateExpiration.Split('/');
    if (parts.Length != 2 || 
        !int.TryParse(parts[0], out int mois) || 
        !int.TryParse(parts[1], out int annee))
    {
        return Results.BadRequest(new { succes = false, message = "Format de date invalide (MM/YY)." });
    }

    var dateExpiration = new DateTime(2000 + annee, mois, 1).AddMonths(1).AddDays(-1);
    if (dateExpiration < DateTime.Today)
    {
        return Results.BadRequest(new { succes = false, message = "La carte est expirée." });
    }

    // 2. Vérification du numéro avec l'algorithme de Luhn
    if (!EstNumeroCarteValide(req.NumeroCarte))
    {
        return Results.BadRequest(new { succes = false, message = "Numéro de carte invalide." });
    }

    // Si tout est bon, le paiement est accepté !
    return Results.Ok(new { succes = true, message = "Paiement validé avec succès." });
});

app.Run();

// --- 1. Fonctions (DOIVENT être avant les Records/Classes) ---
static bool EstNumeroCarteValide(string numero)
{
    if (string.IsNullOrWhiteSpace(numero)) return false;
    numero = numero.Replace(" ", ""); // Enlève les espaces
    if (!numero.All(char.IsDigit)) return false;

    int sum = 0;
    bool alternate = false;
    for (int i = numero.Length - 1; i >= 0; i--)
    {
        int n = int.Parse(numero[i].ToString());
        if (alternate)
        {
            n *= 2;
            if (n > 9) n = (n % 10) + 1;
        }
        sum += n;
        alternate = !alternate;
    }
    return (sum % 10 == 0);
}

// --- 2. Objets et Types (DOIVENT être tout à la fin du fichier) ---
public record PaiementRequest(string NumeroCarte, string DateExpiration, decimal Montant);