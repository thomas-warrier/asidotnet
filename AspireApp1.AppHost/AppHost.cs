var builder = DistributedApplication.CreateBuilder(args);

// --- 1. INFRASTRUCTURE (Base de données & Cache) ---
var cache = builder.AddRedis("cache");

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin(); // pour avoir une interface de gestion DB

var database = postgres.AddDatabase("RentalDb");

// A. On déclare d'abord le service de paiement
var paiementService = builder.AddProject<Projects.AspireApp1_PaiementService>("paiementservice");

var apiService = builder.AddProject<Projects.AspireApp1_ApiService>("apiservice")
    .WithHttpHealthCheck("/health")
    .WithReference(database)         // Accès à la base de données
    .WaitFor(database)               // Attend que la base soit prête avant de démarrer l'API
    .WithReference(paiementService); // Accès au service de paiement


builder.AddProject<Projects.AspireApp1_Gateway>("gateway")
    .WithReference(apiService) 
    .WithExternalHttpEndpoints();

// Front-end (Web)
builder.AddProject<Projects.AspireApp1_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();