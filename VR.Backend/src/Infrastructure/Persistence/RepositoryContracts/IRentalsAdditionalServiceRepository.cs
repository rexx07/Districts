using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IRentalsAdditionalServiceRepository : IAsyncRepository<RentalsAdditionalService>,
                                                       IRepository<RentalsAdditionalService>
{
}