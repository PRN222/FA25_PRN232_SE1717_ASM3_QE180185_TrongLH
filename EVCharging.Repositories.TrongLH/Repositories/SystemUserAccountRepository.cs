using EVCharging.Repositories.TrongLH.Basic;
using EVCharging.Repositories.TrongLH.Context;
using EVCharging.Repositories.TrongLH.Models;
using Microsoft.EntityFrameworkCore;

namespace EVCharging.Repositories.TrongLH.Repositories;

public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
{
    public SystemUserAccountRepository()
    {
    }

    public SystemUserAccountRepository(FA25_PRN232_SE1717_G2_EVChargingContext context) : base(context)
    {
    }

    public async Task<SystemUserAccount?> GetUserAccountAsync(string userName, string password)
    {
        return await _context.SystemUserAccounts.FirstOrDefaultAsync(u =>
            u.UserName == userName && u.Password == password && u.IsActive == true);

        // return await _context.SystemUserAccounts.FirstOrDefaultAsync(u =>
        //     u.Phone == userName && u.Password == password && u.IsActive == true);
        // return await _context.SystemUserAccounts.FirstOrDefaultAsync(u =>
        //     u.Email == userName && u.Password == password && u.IsActive == true);
        // return await _context.SystemUserAccounts.FirstOrDefaultAsync(u =>
        //     u.EmployeeCode == userName && u.Password == password && u.IsActive == true);
    }
}