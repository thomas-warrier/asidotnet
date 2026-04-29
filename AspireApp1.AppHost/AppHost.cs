var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

// Ajout du serveur PostgreSQL et de la base de données
var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin(); // pour avoir une interface de gestion DB

var database = postgres.AddDatabase("RentalDb");

// Configuration de l'API avec référence à la base de données
var apiService = builder.AddProject<Projects.AspireApp1_ApiService>("apiservice")
    .WithHttpHealthCheck("/health")
    .WithReference(database); // On passe la référence de la DB à l'ApiService

// Configuration du Front-end (Web)
builder.AddProject<Projects.AspireApp1_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();