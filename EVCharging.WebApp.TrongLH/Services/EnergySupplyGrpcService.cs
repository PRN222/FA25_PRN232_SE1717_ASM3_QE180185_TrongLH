using EVCharging.WebApp.TrongLH.Models;
using Grpc.Net.Client;

namespace EVCharging.WebApp.TrongLH.Services;

public class EnergySupplyGrpcService
{
    private readonly EnergySupplyTrongLhGRPC.EnergySupplyTrongLhGRPCClient _grpcClient;
    private readonly ILogger<EnergySupplyGrpcService> _logger;

    public EnergySupplyGrpcService(IConfiguration configuration, ILogger<EnergySupplyGrpcService> logger)
    {
        _logger = logger;
        var grpcUrl = configuration["GrpcSettings:ServerUrl"] ?? "https://localhost:7146";

        // Use HTTPS + HTTP/2 and trust local dev certificate
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        var channel = GrpcChannel.ForAddress(grpcUrl, new GrpcChannelOptions { HttpHandler = handler });

        _grpcClient = new EnergySupplyTrongLhGRPC.EnergySupplyTrongLhGRPCClient(channel);
    }

    public async Task<List<EnergySupplyViewModel>> GetAllAsync()
    {
        try
        {
            var response = _grpcClient.GetAllAsync(new EmptyRequest());
            var result = new List<EnergySupplyViewModel>();

            foreach (var item in response.EnergySupplies)
            {
                result.Add(new EnergySupplyViewModel
                {
                    EnergySupplyTrongLhid = item.EnergySupplyTrongLhid,
                    StationTrongLhid = item.StationTrongLhid,
                    SupplyType = item.SupplyType,
                    CapacityKw = item.CapacityKw,
                    AvailableKw = item.AvailableKw,
                    SourceName = item.SourceName,
                    StartDate = item.StartDate,
                    ContractNumber = item.ContractNumber,
                    IsRenewable = item.IsRenewable,
                    PeakCapacity = item.PeakCapacity,
                    EfficiencyRate = item.EfficiencyRate,
                    CreatedAt = item.CreatedAt
                });
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all energy supplies from gRPC service");
            throw;
        }
    }

    public async Task<EnergySupplyViewModel?> GetByIdAsync(int id)
    {
        try
        {
            var response = _grpcClient.GetByIdAsync(new IdRequest { Id = id });

            if (response.EnergySupplyTrongLhid == 0)
                return null;

            return new EnergySupplyViewModel
            {
                EnergySupplyTrongLhid = response.EnergySupplyTrongLhid,
                StationTrongLhid = response.StationTrongLhid,
                SupplyType = response.SupplyType,
                CapacityKw = response.CapacityKw,
                AvailableKw = response.AvailableKw,
                SourceName = response.SourceName,
                StartDate = response.StartDate,
                ContractNumber = response.ContractNumber,
                IsRenewable = response.IsRenewable,
                PeakCapacity = response.PeakCapacity,
                EfficiencyRate = response.EfficiencyRate,
                CreatedAt = response.CreatedAt
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting energy supply by ID from gRPC service");
            throw;
        }
    }

    public async Task<int> CreateAsync(EnergySupplyViewModel model)
    {
        try
        {
            var request = new EnergySupplyTrongLh
            {
                EnergySupplyTrongLhid = 0, // Will be ignored by server (identity column)
                StationTrongLhid = model.StationTrongLhid,
                SupplyType = model.SupplyType,
                CapacityKw = model.CapacityKw,
                AvailableKw = model.AvailableKw,
                SourceName = model.SourceName,
                StartDate = model.StartDate ?? string.Empty,
                ContractNumber = model.ContractNumber ?? string.Empty,
                IsRenewable = model.IsRenewable,
                PeakCapacity = model.PeakCapacity ?? 0,
                EfficiencyRate = model.EfficiencyRate ?? 0,
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            var response = _grpcClient.CreateAsync(request);
            return response.AffectedRows;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating energy supply via gRPC service");
            throw;
        }
    }

    public async Task<int> UpdateAsync(EnergySupplyViewModel model)
    {
        try
        {
            var request = new EnergySupplyTrongLh
            {
                EnergySupplyTrongLhid = model.EnergySupplyTrongLhid ?? 0,
                StationTrongLhid = model.StationTrongLhid,
                SupplyType = model.SupplyType,
                CapacityKw = model.CapacityKw,
                AvailableKw = model.AvailableKw,
                SourceName = model.SourceName,
                StartDate = model.StartDate ?? string.Empty,
                ContractNumber = model.ContractNumber ?? string.Empty,
                IsRenewable = model.IsRenewable,
                PeakCapacity = model.PeakCapacity ?? 0,
                EfficiencyRate = model.EfficiencyRate ?? 0,
                CreatedAt = model.CreatedAt ?? string.Empty
            };

            var response = _grpcClient.UpdateAsync(request);
            return response.AffectedRows;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating energy supply via gRPC service");
            throw;
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        try
        {
            var response = _grpcClient.DeleteAsync(new IdRequest { Id = id });
            return response.AffectedRows;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting energy supply via gRPC service");
            throw;
        }
    }
}
