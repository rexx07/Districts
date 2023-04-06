using Domain.Entities;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IOtpAuthenticatorRepository : IAsyncRepository<OtpAuthenticator>, IRepository<OtpAuthenticator>
{
}