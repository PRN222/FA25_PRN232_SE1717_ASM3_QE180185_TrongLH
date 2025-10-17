namespace EVCharging.Services.TrongLH.Interfaces;

public interface IServiceProviders
{
    IEnergySupplyTrongLhService EnergySupplyTrongLhService { get;}
    IStationTrongLHService StationTrongLhService { get; }
    ISystemUserAccountService SystemUserAccountService { get; }
}