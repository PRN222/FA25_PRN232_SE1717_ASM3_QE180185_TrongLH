using System.Text.Json;
using System.Text.Json.Serialization;
using EVCharging.Services.TrongLH.Interfaces;
using Grpc.Core;

namespace EVCharing.gRPCService.TrongLH.Services;

public class EnergySupplyTrongLhServiceGrpcService(
    ILogger<EnergySupplyTrongLhServiceGrpcService> logger,
    IServiceProviders service)
    : EnergySupplyTrongLhGRPC.EnergySupplyTrongLhGRPCBase
{
    private readonly ILogger<EnergySupplyTrongLhServiceGrpcService> _logger = logger;
    private readonly IServiceProviders _service = service;

    public override async Task<EnergySupplyTrongLhListList> GetAllAsync(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            var energySupplies = await _service.EnergySupplyTrongLhService.GetAllAsync();

            var opt = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var json = JsonSerializer.Serialize(energySupplies, opt);
            var result = new EnergySupplyTrongLhListList();

            var items = JsonSerializer.Deserialize<List<EnergySupplyTrongLh>>(json, opt);
            if (items != null) result.EnergySupplies.AddRange(items);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAllAsync");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<EnergySupplyTrongLh> GetByIdAsync(IdRequest request, ServerCallContext context)
    {
        try
        {
            var energySupply = await _service.EnergySupplyTrongLhService.GetByIdAsync(request.Id);

            var opt = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var json = JsonSerializer.Serialize(energySupply, opt);
            var result = JsonSerializer.Deserialize<EnergySupplyTrongLh>(json, opt);

            return result ?? new EnergySupplyTrongLh();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetByIdAsync");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override Task<EnergySupplyTrongLh> CreateAsync(EnergySupplyTrongLh request, ServerCallContext context)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in CreateAsync");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }
}