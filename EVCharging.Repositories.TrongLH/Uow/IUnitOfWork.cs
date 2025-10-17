using EVCharging.Repositories.TrongLH.Repositories;

namespace EVCharging.Repositories.TrongLH.Uow;

public abstract class IUnitOfWork : IDisposable
{
    public abstract EnergySupplyTrongLHRepository EnergySupplyTrongLHRepository { get; }
    public abstract StationTrongLHRepository StationTrongLHRepository { get; }
    public abstract SystemUserAccountRepository SystemUserAccountRepository { get; }
    public abstract int SaveChangesWithTransaction();
    public abstract Task<int> SaveChangesWithTransactionAsync();
    public abstract void Dispose();
}