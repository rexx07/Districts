using Core.Domain.Entities.Land;

namespace Modules.BaseApplication.Services.ModelService;

public interface IModelService
{
    public Task<Model> GetById(int id);
}