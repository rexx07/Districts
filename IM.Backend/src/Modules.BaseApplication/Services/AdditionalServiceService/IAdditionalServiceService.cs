using Core.Domain.Entities;

namespace Modules.BaseApplication.Services.AdditionalServiceService;

public interface IAdditionalServiceService
{
    public Task<IList<AdditionalService>> GetListByIds(int[] ids);
}