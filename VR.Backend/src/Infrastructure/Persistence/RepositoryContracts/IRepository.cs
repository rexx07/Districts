﻿using System.Linq.Expressions;
using Domain.Entities;
using Infrastructure.Persistence.Dynamic;
using Infrastructure.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Persistence.RepositoryContracts;

public interface IRepository<T> : IQuery<T>
    where T : Entity
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
    IList<T> AddRange(IList<T> entities);
    T Update(T entity);
    IList<T> UpdateRange(IList<T> entities);
    T Delete(T entity);
    IList<T> DeleteRange(IList<T> entity);
}