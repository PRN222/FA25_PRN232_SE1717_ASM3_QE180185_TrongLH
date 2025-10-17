using EVCharging.Repositories.TrongLH.Models;
using EVCharging.Repositories.TrongLH.Uow;
using EVCharging.Services.TrongLH.Interfaces;

namespace EVCharging.Services.TrongLH;

public class StationTrongLhService : IStationTrongLHService
{
    // private readonly StationTrongLHRepository _repository = new();
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

    public async Task<List<StationTrongLh>> GetAllStationsAsync()
    {
        try
        {
            var stations = await _unitOfWork.StationTrongLHRepository.GetAllAsync();
            return stations;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}