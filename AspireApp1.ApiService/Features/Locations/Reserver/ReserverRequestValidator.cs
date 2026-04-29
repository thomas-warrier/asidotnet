using FluentValidation;

namespace AspireApp1.ApiService.Features.Locations.Reserver;

public class ReserverRequestValidator : AbstractValidator<ReserverRequest>
{
    public ReserverRequestValidator()
    {
        RuleFor(x => x.Immatriculation)
            .NotEmpty().WithMessage("L'immatriculation est obligatoire.")
            .Matches(@"^[A-Z]{2}-\d{3}-[A-Z]{2}$").WithMessage("Le format doit être type AA-123-BB.");

        RuleFor(x => x.LoueurId)
            .GreaterThan(0).WithMessage("L'ID du loueur est invalide.");

        // Validation croisée : DateFin doit être >= DateDebut
        RuleFor(x => x.DateFin)
            .GreaterThanOrEqualTo(x => x.DateDebut).WithMessage("La date de fin doit être après le début.");
    }
}