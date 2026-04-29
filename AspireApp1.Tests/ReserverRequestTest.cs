using AspireApp1.ApiService.Features.Locations.Reserver;
using FluentAssertions;
using FluentValidation.TestHelper; // Ajoute des méthodes d'extension utiles pour les tests

namespace AspireApp1.Tests.Validators;

[TestFixture]
public class ReserverRequestValidatorTests
{
    private ReserverRequestValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new ReserverRequestValidator();
    }

    [Test]
    public void Immatriculation_FormatInvalide_RetourneErreur()
    {
        var request = new ReserverRequest("AB123CD", 1, DateTime.Now, DateTime.Now.AddDays(1));
        
        var result = _validator.TestValidate(request);
        
        result.ShouldHaveValidationErrorFor(x => x.Immatriculation)
            .WithErrorMessage("Le format doit être type AA-123-BB.");
    }

    [Test]
    public void Request_Valide_NeRetourneAucuneErreur()
    {
        var request = new ReserverRequest("AA-123-BB", 1, DateTime.Now, DateTime.Now.AddDays(1));
        
        var result = _validator.TestValidate(request);
        
        result.ShouldNotHaveAnyValidationErrors();
    }
}