using EVCharging.Repositories.TrongLH.Context;
using EVCharging.Repositories.TrongLH.Repositories;

namespace EVCharging.Repositories.TrongLH.Uow;

public class UnitOfWork : IUnitOfWork
{
    private readonly FA25_PRN232_SE1717_G2_EVChargingContext _context;
    private EnergySupplyTrongLHRepository _energySupplyTrongLHRepository;
    private StationTrongLHRepository _stationTrongLHRepository;
    private SystemUserAccountRepository _systemUserAccountRepository;

    public UnitOfWork()
    {
        _context = new FA25_PRN232_SE1717_G2_EVChargingContext();
    }

    public override EnergySupplyTrongLHRepository EnergySupplyTrongLHRepository
    {
        get { return _energySupplyTrongLHRepository ??= new EnergySupplyTrongLHRepository(_context); }
    }

    public override StationTrongLHRepository StationTrongLHRepository
    {
        get { return _stationTrongLHRepository ??= new StationTrongLHRepository(_context); }
    }

    public override SystemUserAccountRepository SystemUserAccountRepository
    {
        get { return _systemUserAccountRepository ??= new SystemUserAccountRepository(_context); }
    }

    public override void Dispose()
    {
        throw new NotImplementedException();
    }

    public override int SaveChangesWithTransaction()
    {
        var result = -1;

        //System.Data.IsolationLevel.Snapshot
        using (var dbContextTransaction = _context.Database.BeginTransaction())
        {
            try
            {
                result = _context.SaveChanges();
                dbContextTransaction.Commit();
            }
            catch (Exception)
            {
                //Log Exception Handling message                      
                result = -1;
                dbContextTransaction.Rollback();
            }
        }

        return result;
    }

    public override async Task<int> SaveChangesWithTransactionAsync()
    {
        var result = -1;

        //System.Data.IsolationLevel.Snapshot
        using (var dbContextTransaction = _context.Database.BeginTransaction())
        {
            try
            {
                result = await _context.SaveChangesAsync();
                dbContextTransaction.Commit();
            }
            catch (Exception)
            {
                //Log Exception Handling message                      
                result = -1;
                dbContextTransaction.Rollback();
            }
        }

        return result;
    }
}