using EVCharging.Repositories.TrongLH.Basic;
using EVCharging.Repositories.TrongLH.Context;
using EVCharging.Repositories.TrongLH.Models;

namespace EVCharging.Repositories.TrongLH.Repositories;

public class StationTrongLHRepository : GenericRepository<StationTrongLh>
{
    public StationTrongLHRepository()
    {
    }

    public StationTrongLHRepository(FA25_PRN232_SE1717_G2_EVChargingContext context) : base(context)
    {
    }
}