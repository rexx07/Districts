using Core.Domain.Entities;

namespace Modules.BaseApplication.Services.CustomerService;

public interface ICustomerService
{
    public Task<Customer?> GetByUserId(int userId);
}