using FluentValidation;

namespace AspireApp1.ApiService.Features.Loueurs.Creer;

public class CreerLoueurValidator : AbstractValidator<CreerLoueurRequest>
{
    public CreerLoueurValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom est obligatoire.")
            .MaximumLength(100);

        RuleFor(x => x.Prenom)
            .NotEmpty().WithMessage("Le prénom est obligatoire.")
            .MaximumLength(100);

        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("Le mobile est obligatoire.")
            .Matches(@"^0[1-9]\d{8}$").WithMessage("Format de mobile attendu : 10 chiffres commençant par 0.")
            .MaximumLength(10);

        RuleFor(x => x.Cp)
            .Matches(@"^\d{5}$").When(x => !string.IsNullOrEmpty(x.Cp))
            .WithMessage("Le code postal doit contenir 5 chiffres.");
            
        RuleFor(x => x.Pays)
            .MaximumLength(50);
    }
}