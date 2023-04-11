using System.Linq.Expressions;
using Core.Domain.Entities.BaseEntities;
using Core.Infrastructure.Persistence.Dynamic;
using Core.Infrastructure.Persistence.Paging;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Infrastructure.Persistence.Repositories;

public class EfRepositoryBase<TEntity, TContext> : IAsyncRepository<TEntity>, IRepository<TEntity>
    where TEntity : Aggregate<long>
    where TContext : DbContext
{
    protected readonly TContext Context;

    public EfRepositoryBase(TContext context)
    {
        Context = context;
    }

    public IQueryable<TEntity> Query() => Context.Set<TEntity>();

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken=default)
    {
        await Context.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IList<TEntity>> BulkAddAsync(IList<TEntity> entities, CancellationToken cancellationToken)
    {
        await Context.BulkInsertAsync(entities, cancellationToken: cancellationToken);
        await Context.BulkSaveChangesAsync(cancellationToken: cancellationToken);
        return entities;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Context.Update(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IList<TEntity>> BulkUpdateAsync(IList<TEntity> entities, CancellationToken cancellationToken)
    {
        
        await Context.BulkUpdateAsync(entities, cancellationToken: cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entities;
    }

    public async Task<IList<TEntity>> BulkAddUpdateAsync(IList<TEntity> entities,
                                                         CancellationToken cancellationToken)
    {
        await Context.BulkInsertOrUpdateAsync(entities, cancellationToken: cancellationToken);
        await Context.BulkSaveChangesAsync(cancellationToken: cancellationToken);
        return entities;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IList<TEntity>> BulkDeleteAsync(IList<TEntity> entities,
                                                      CancellationToken cancellationToken)
    {
        await Context.BulkDeleteAsync(entities, cancellationToken: cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entities;
    }
    
    public async Task<IList<TEntity>> BulkAddUpdateDeleteAsync(IList<TEntity> entities,
                                                               CancellationToken cancellationToken)
    {
        await Context.BulkInsertOrUpdateOrDeleteAsync(entities, cancellationToken: cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entities;
    }

    public async Task<IPaginate<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (orderBy != null)
            return await orderBy(queryable).ToPaginateAsync(index, size, from: 0, cancellationToken);
        return await queryable.ToPaginateAsync(index, size, from: 0, cancellationToken);
    }

    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<IPaginate<TEntity>> GetListByDynamicAsync(
        DynamicQuery dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> queryable = Query().ToDynamic(dynamic);
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (predicate != null)
            queryable = queryable.Where(predicate);
        return await queryable.ToPaginateAsync(index, size, from: 0, cancellationToken);
    }

    public async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (predicate is not null)
            queryable = queryable.Where(predicate);
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        return await queryable.AnyAsync(cancellationToken);
    }

    public TEntity Add(TEntity entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
        return entity;
    }

    public IList<TEntity> BulkAdd(IList<TEntity> entities)
    {
        Context.BulkInsert(entities);
        Context.BulkSaveChanges();
        return entities;
    }

    public TEntity Update(TEntity entity)
    {
        Context.Update(entity);
        Context.SaveChanges();
        return entity;
    }

    public IList<TEntity> BulkUpdate(IList<TEntity> entities)
    {
        Context.BulkUpdate(entities);
        Context.BulkSaveChanges();
        return entities;
    }
    
    public IList<TEntity> BulkAddUpdate(IList<TEntity> entities)
    {
        Context.BulkInsertOrUpdate(entities);
        Context.BulkSaveChanges();
        return entities;
    }

    public TEntity Delete(TEntity entity)
    {
        Context.Remove(entity);
        Context.SaveChanges();
        return entity;
    }

    public IList<TEntity> BulkDelete(IList<TEntity> entities)
    {
        Context.BulkDelete(entities);
        Context.BulkSaveChanges();
        return entities;
    }

    public IList<TEntity> BulkAddUpdateDelete(IList<TEntity> entities) 
    {
        Context.BulkInsertOrUpdateOrDelete(entities);
        Context.BulkSaveChanges();
        return entities;
    }

    public TEntity? Get(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = true
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        return queryable.FirstOrDefault(predicate);
    }

    public IPaginate<TEntity> GetList(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool enableTracking = true
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (orderBy != null)
            return orderBy(queryable).ToPaginate(index, size);
        return queryable.ToPaginate(index, size);
    }

    public IPaginate<TEntity> GetListByDynamic(
        DynamicQuery dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool enableTracking = true
    )
    {
        IQueryable<TEntity> queryable = Query().ToDynamic(dynamic);
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (predicate != null)
            queryable = queryable.Where(predicate);
        return queryable.ToPaginate(index, size);
    }

    public bool Any(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query();
        if (predicate is not null)
            queryable = queryable.Where(predicate);
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        return queryable.Any();
    }
}