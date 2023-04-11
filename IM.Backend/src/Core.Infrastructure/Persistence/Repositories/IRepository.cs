using System.Linq.Expressions;
using Core.Domain.Entities;
using Core.Domain.Entities.BaseEntities;
using Core.Infrastructure.Persistence.Dynamic;
using Core.Infrastructure.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Infrastructure.Persistence.Repositories;

public interface IRepository<T> : IQuery<T>
    where T : Aggregate<long>
{
    T? Get(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool enableTracking = true
    );

    IPaginate<T> GetList(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int index = 0,
        int size = 10,
        bool enableTracking = true
    );

    IPaginate<T> GetListByDynamic(
        DynamicQuery dynamic,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int index = 0,
        int size = 10,
        bool enableTracking = true
    );

    bool Any(Expression<Func<T, bool>>? predicate = null, bool enableTracking = true);
    T Add(T entity);
    IList<T> BulkAdd(IList<T> entities);
    T Update(T entity);
    IList<T> BulkUpdate(IList<T> entities);
    IList<T> BulkAddUpdate(IList<T> entities);
    T Delete(T entity);
    IList<T> BulkDelete(IList<T> entity);
    IList<T> BulkAddUpdateDelete(IList<T> entities);
}