namespace Infrastructure.Persistence.RepositoryContracts;

public interface IQuery<T>
{
    IQueryable<T> Query();
}