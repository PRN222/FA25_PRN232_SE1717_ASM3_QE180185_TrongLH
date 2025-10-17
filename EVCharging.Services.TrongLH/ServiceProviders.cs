using EVCharging.Services.TrongLH.Interfaces;

namespace EVCharging.Services.TrongLH;

public class ServiceProviders : IServiceProviders
{
    private IEnergySupplyTrongLhService _energySupplyTrongLhService;
    private IStationTrongLHService _stationTrongLhService;
    private ISystemUserAccountService _systemUserAccountService;

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