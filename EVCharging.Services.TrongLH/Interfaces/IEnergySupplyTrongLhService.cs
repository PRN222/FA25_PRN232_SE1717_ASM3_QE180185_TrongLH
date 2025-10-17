using EVCharging.Repositories.TrongLH.ModelExtensions;
using EVCharging.Repositories.TrongLH.Models;

namespace EVCharging.Services.TrongLH.Interfaces;

public interface IEnergySupplyTrongLhService
{
    Task<List<EnergySupplyTrongLh>> GetAllAsync();

    Task<PaginationResult<List<EnergySupplyTrongLh>>> GetAllAsync(string? supplyType, decimal? capacityKw,
        string? stationName);

    Task<PaginationResult<List<EnergySupplyTrongLh>>> GetAllAsync(EnergySupplyTrongLhSearchRequest searchRequest);
    Task<EnergySupplyTrongLh?> GetByIdAsync(int id);
    Task<int> CreateAsync(EnergySupplyTrongLh energySupplyTrongLh);
    Task<int> UpdateAsync(EnergySupplyTrongLh energySupplyTrongLh);
    Task<bool> DeleteAsync(int id);
}