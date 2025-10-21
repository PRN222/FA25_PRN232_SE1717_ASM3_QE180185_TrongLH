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

    public override async Task<MutationRelay> CreateAsync(EnergySupplyTrongLh request, ServerCallContext context)
    {
        try
        {
            var opt = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var energySupplyEntity = JsonSerializer.Serialize(request, opt);
            var item =
                JsonSerializer.Deserialize<EVCharging.Repositories.TrongLH.Models.EnergySupplyTrongLh>(
                    energySupplyEntity, opt);
            if (item == null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid payload"));
            // Ensure EF treats this as a new entity with DB-generated identity
            item.EnergySupplyTrongLhid = null;

            var createdEnergySupply = await _service.EnergySupplyTrongLhService.CreateAsync(item);
            return new MutationRelay { AffectedRows = createdEnergySupply };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in CreateAsync");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<MutationRelay> UpdateAsync(EnergySupplyTrongLh request, ServerCallContext context)
    {
        try
        {
            var opt = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var energySupplyEntity = JsonSerializer.Serialize(request, opt);
            var item =
                JsonSerializer.Deserialize<EVCharging.Repositories.TrongLH.Models.EnergySupplyTrongLh>(
                    energySupplyEntity, opt);

            var createdEnergySupply = await _service.EnergySupplyTrongLhService.UpdateAsync(item);
            return new MutationRelay { AffectedRows = createdEnergySupply };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in CreateAsync");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }

    public override async Task<MutationRelay> DeleteAsync(IdRequest request, ServerCallContext context)
    {
        try
        {
            var deletedEnergySupply = await _service.EnergySupplyTrongLhService.DeleteAsync(request.Id);
            return new MutationRelay { AffectedRows = deletedEnergySupply ? 1 : 0 };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in DeleteAsync");
            throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
        }
    }
}