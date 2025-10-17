using EVCharging.Repositories.TrongLH.ModelExtensions;
using EVCharging.Repositories.TrongLH.Models;
using EVCharging.Repositories.TrongLH.Uow;
using EVCharging.Services.TrongLH.Interfaces;

namespace EVCharging.Services.TrongLH;

public class EnergySupplyTrongLhService : IEnergySupplyTrongLhService
{
    public async Task<List<EnergySupplyTrongLh>> GetAllAsync()
    {
        try
        {
            var uow = new UnitOfWork();
            return await uow.EnergySupplyTrongLHRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<PaginationResult<List<EnergySupplyTrongLh>>> GetAllAsync(string? supplyType, decimal? capacityKw,
        string? stationName)
    {
        var searchRequest = new EnergySupplyTrongLhSearchRequest(supplyType, capacityKw, stationName, null, null);
        // Gọi phương thức GetAllAsync với đối tượng searchRequest
        return await GetAllAsync(searchRequest);
    }

    public async Task<PaginationResult<List<EnergySupplyTrongLh>>> GetAllAsync(
        EnergySupplyTrongLhSearchRequest searchRequest)
    {
        try
        {
            var uow = new UnitOfWork();
            var energySupplies = await uow.EnergySupplyTrongLHRepository.GetAllAsync(searchRequest);
            return energySupplies;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<EnergySupplyTrongLh?> GetByIdAsync(int id)
    {
        try
        {
            var uow = new UnitOfWork();
            return await uow.EnergySupplyTrongLHRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<int> CreateAsync(EnergySupplyTrongLh energySupplyTrongLh)
    {
        try
        {
            var uow = new UnitOfWork();
            return uow.EnergySupplyTrongLHRepository.CreateAsync(energySupplyTrongLh);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<int> UpdateAsync(EnergySupplyTrongLh energySupplyTrongLh)
    {
        try
        {
            var uow = new UnitOfWork();
            return uow.EnergySupplyTrongLHRepository.UpdateAsync(energySupplyTrongLh);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var uow = new UnitOfWork();
            var energySupply = await uow.EnergySupplyTrongLHRepository.GetByIdAsync(id);
            return await (energySupply == null
                ? throw new Exception("Energy supply not found")
                : uow.EnergySupplyTrongLHRepository.RemoveAsync(energySupply));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}