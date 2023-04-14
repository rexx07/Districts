using Core.Domain.Entities;

namespace Modules.BaseApplication.Services.FindeksCreditRateService;

public interface IFindeksCreditRateService
{
    public Task<FindeksCreditRate> GetFindeksCreditRateByCustomerId(int customerId);

    public Task<FindeksCreditRate> Add(FindeksCreditRate findeksCreditRate);
}