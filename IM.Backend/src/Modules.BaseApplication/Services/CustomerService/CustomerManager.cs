using Core.Domain.Entities;

namespace Modules.BaseApplication.Services.CustomerService;

public class CustomerManager : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerManager(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer?> GetByUserId(int userId)
    {
        Customer? customer = await _customerRepository.GetAsync(c => c.UserId == userId);
        return customer;
    }
}