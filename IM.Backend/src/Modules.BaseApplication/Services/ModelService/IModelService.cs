using Core.Domain.Entities;
using Core.Domain.Entities.Land;

namespace Application.Services.ModelService;

public interface IModelService
{
    public Task<Model> GetById(int id);
}