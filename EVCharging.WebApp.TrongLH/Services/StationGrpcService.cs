using EVCharging.WebApp.TrongLH.Models;
using Grpc.Net.Client;

namespace EVCharging.WebApp.TrongLH.Services;

public class StationGrpcService
{
    private readonly StationTrongLhGRPC.StationTrongLhGRPCClient _grpcClient;
    private readonly ILogger<StationGrpcService> _logger;

    public StationGrpcService(IConfiguration configuration, ILogger<StationGrpcService> logger)
    {
        _logger = logger;
        var grpcUrl = configuration["GrpcSettings:ServerUrl"] ?? "http://localhost:5001";
        var channel = GrpcChannel.ForAddress(grpcUrl);
        _grpcClient = new StationTrongLhGRPC.StationTrongLhGRPCClient(channel);
    }

    public async Task<List<StationOption>> GetAllStationsAsync()
    {
        try
        {
            var response = _grpcClient.GetAllAsync(new StationEmptyRequest());
            return response.Stations.Select(s => new StationOption
            {
                Id = s.StationTrongLhid,
                Name = s.Name,
                Code = s.Code,
                Location = s.Location
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting stations via gRPC");
            throw;
        }
    }

    public async Task<StationOption?> GetStationByIdAsync(int id)
    {
        try
        {
            var s = _grpcClient.GetByIdAsync(new StationIdRequest { Id = id });
            if (s.StationTrongLhid == 0) return null;
            return new StationOption
            {
                Id = s.StationTrongLhid,
                Name = s.Name,
                Code = s.Code,
                Location = s.Location
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting station by id via gRPC");
            throw;
        }
    }
}


