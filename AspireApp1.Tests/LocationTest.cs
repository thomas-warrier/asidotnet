using AspireApp1.ApiService.Features.Locations.Service;
using FluentAssertions;

namespace AspireApp1.Tests.Services;

[TestFixture]
public class LocationServiceTests
{
    private LocationService _service;

    [SetUp]
    public void Setup()
    {
        _service = new LocationService();
    }

    [Test]
    public void CalculerPrixTotal_TroisJours_RetourneLeBonPrix()
    {
        // Arrange
        var debut = new DateTime(2026, 5, 1);
        var fin = new DateTime(2026, 5, 3); // 1, 2 et 3 mai = 3 jours

        // Act
        var prix = _service.CalculerPrixTotal(120m, debut, fin);

        // Assert
        prix.Should().Be(360m); // 120 * 3
    }

    [Test]
    public void CalculerPrixTotal_MemeJour_FactureUnJour()
    {
        // Arrange
        var date = new DateTime(2026, 5, 1);

        // Act
        var prix = _service.CalculerPrixTotal(120m, date, date);

        // Assert
        prix.Should().Be(120m);
    }

    [Test]
    public void CalculerPrixTotal_ChangementAnnee_CalculeCorrectement()
    {
        // Arrange
        var debut = new DateTime(2025, 12, 31);
        var fin = new DateTime(2026, 1, 1); // 2 jours

        // Act
        var prix = _service.CalculerPrixTotal(100m, debut, fin);

        // Assert
        prix.Should().Be(200m);
    }

    [Test]
    public void CalculerPrixTotal_DateFinAvantDateDebut_LeveException()
    {
        // Arrange
        var debut = new DateTime(2026, 5, 5);
        var fin = new DateTime(2026, 5, 1); // Impossible

        // Act
        Action act = () => _service.CalculerPrixTotal(120m, debut, fin);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("La date de fin ne peut pas être antérieure à la date de début.");
    }
}