using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IFindeksCreditRateRepository : IAsyncRepository<FindeksCreditRate>, IRepository<FindeksCreditRate>
{
}