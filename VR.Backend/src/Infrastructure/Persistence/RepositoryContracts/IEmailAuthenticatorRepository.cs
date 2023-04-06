using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IEmailAuthenticatorRepository : IAsyncRepository<EmailAuthenticator>, IRepository<EmailAuthenticator>
{
}