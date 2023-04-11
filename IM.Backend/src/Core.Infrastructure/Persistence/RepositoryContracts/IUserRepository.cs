﻿using Core.Domain.Entities.Security;
using Core.Infrastructure.Persistence.Repositories;

namespace Core.Infrastructure.Persistence.RepositoryContracts;

public interface IUserRepository : IAsyncRepository<User>, IRepository<User>
{
}