using EVCharging.Services.TrongLH.Interfaces;

namespace EVCharging.Services.TrongLH;

public class ServiceProviders : IServiceProviders
{
    private ISystemUserAccountService _systemUserAccountService;
    private IEnergySupplyTrongLhService _energySupplyTrongLhService;
    private IStationTrongLHService _stationTrongLhService;
    
    public IEnergySupplyTrongLhService EnergySupplyTrongLhService
    {
        get { return _energySupplyTrongLhService ??= new EnergySupplyTrongLhService(); }
    }
    
    public IStationTrongLHService StationTrongLhService 
    {
        get { return _stationTrongLhService ??= new StationTrongLhService(); }
    }
    
    public ISystemUserAccountService SystemUserAccountService
    {
        get { return _systemUserAccountService ??= new SystemUserAccountService(); }
    }
}