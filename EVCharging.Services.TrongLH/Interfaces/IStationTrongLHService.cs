using EVCharging.Repositories.TrongLH.Models;

namespace EVCharging.Services.TrongLH.Interfaces;

public interface IStationTrongLHService
{
    Task<List<StationTrongLh>> GetAllStationsAsync();
}