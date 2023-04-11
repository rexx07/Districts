using System.Linq.Expressions;
using Core.Domain.Entities;
using Core.Domain.Entities.BaseEntities;
using Core.Infrastructure.Persistence.Dynamic;
using Core.Infrastructure.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Infrastructure.Persistence.Repositories;

public interface IAsyncRepository<T> : IQuery<T>
    where T : Aggregate<long>
{
    Task<T?> GetAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<IPaginate<T>> GetListAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int index = 0,
        int size = 10,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<IPaginate<T>> GetListByDynamicAsync(
        DynamicQuery dynamic,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int index = 0,
        int size = 10,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<bool> AnyAsync(
        Expression<Func<T, bool>>? predicate = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    Task<IList<T>> BulkAddAsync(IList<T> entities, CancellationToken cancellationToken = default);

    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task<IList<T>> BulkUpdateAsync(IList<T> entities, CancellationToken cancellationToken = default);

    Task<IList<T>> BulkAddUpdateAsync(IList<T> entities, CancellationToken cancellationToken = default);

    Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task<IList<T>> BulkDeleteAsync(IList<T> entities, CancellationToken cancellationToken = default);

    Task<IList<T>> BulkAddUpdateDeleteAsync(IList<T> entities, CancellationToken cancellationToken = default);
}