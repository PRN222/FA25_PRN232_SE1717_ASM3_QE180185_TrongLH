using EVCharging.Repositories.TrongLH.Models;
using EVCharging.Repositories.TrongLH.Uow;
using EVCharging.Services.TrongLH.Interfaces;

namespace EVCharging.Services.TrongLH;

public class SystemUserAccountService : ISystemUserAccountService
{
    // private readonly SystemUserAccountRepository _repository = new();
    private readonly IUnitOfWork _unitOfWork = new UnitOfWork();

    public async Task<SystemUserAccount?> LoginAsync(string username, string password)
    {
        try
        {
            var user = await _unitOfWork.SystemUserAccountRepository.GetUserAccountAsync(username, password);
            return user ?? throw new Exception("Invalid username or password.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}