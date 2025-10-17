using EVCharging.Repositories.TrongLH.Models;

namespace EVCharging.Services.TrongLH.Interfaces;

public interface ISystemUserAccountService
{
    Task<SystemUserAccount?> LoginAsync(string username, string password);
}