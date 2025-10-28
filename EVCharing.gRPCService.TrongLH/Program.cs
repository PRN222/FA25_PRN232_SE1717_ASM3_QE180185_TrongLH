using EVCharging.Services.TrongLH;
using EVCharging.Services.TrongLH.Interfaces;
using EVCharing.gRPCService.TrongLH.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddScoped<IServiceProviders, ServiceProviders>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<EnergySupplyTrongLhServiceGrpcService>();
app.MapGrpcService<StationTrongLhGRPCService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();