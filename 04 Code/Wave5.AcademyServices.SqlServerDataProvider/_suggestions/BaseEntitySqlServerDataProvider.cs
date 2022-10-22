using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RCode.Data.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCode.Data.SqlServerDataProviders;

public abstract class BaseEntitySqlServerDataProvider<TEntity, TContext> : IEntityDataProvider<TEntity>
    where TEntity : BaseEntity
    where TContext : DbContext
{
    #region [ Fields ]
    protected readonly ILogger<BaseEntitySqlServerDataProvider<TEntity, TContext>> _logger;

    protected readonly IDbContextFactory<TContext> _dbContextFactory;
    #endregion

    #region [ CTor ]
    public BaseEntitySqlServerDataProvider(
        ILogger<BaseEntitySqlServerDataProvider<TEntity, TContext>> logger,
        IDbContextFactory<TContext> dbContextFactory) {

        this._logger = logger;
        this._dbContextFactory = dbContextFactory;
    }
    #endregion

    #region [ Public Methods - Create / Update / Delete ]
    public virtual async Task AddAsync(TEntity entity) {
        try {
            Guard.ParamIsNull(entity, nameof(entity));

            using var context = this.GetContext();

            var dbEntity = await context.Set<TEntity>().FindAsync(entity.Id);
            if (dbEntity != null) {
                throw DataProviderExceptionFactory.AlreadyExistsException(entity);
            }

            context.Add(entity);
            await context.SaveChangesAsync();


        } catch (Exception ex) {
            throw DataProviderExceptionFactory.AddException(entity, ex);
        }
    }

    public virtual async Task UpdateAsync(TEntity entity) {
        try {
            Guard.ParamIsNull(entity, nameof(entity));

            using var context = this.GetContext();

            var dbEntity = await context.Set<TEntity>().FindAsync(entity.Id);
            if (dbEntity == null) {
                throw DataProviderExceptionFactory.EntityNotFoundException(entity, DatabaseAction.Update, null);
            }

            OnUpdateEntityProperties(entity, dbEntity);
            entity.UpdatedAt = DateTime.UtcNow;

            context.Update(entity);
            await context.SaveChangesAsync();

        } catch (Exception ex) {
            throw DataProviderExceptionFactory.UpdateException(entity, ex);
        }
    }

    public virtual void OnUpdateEntityProperties(TEntity sourceEntity, TEntity databaseEntity) {

        Guard.ParamIsNull(sourceEntity, nameof(sourceEntity));
        Guard.ParamIsNull(databaseEntity, nameof(databaseEntity));

        foreach (var property in databaseEntity.GetType().GetProperties()) {
            var value = property.GetValue(sourceEntity);
            if (property.CanWrite) {
                property.SetValue(databaseEntity, value);
            }
        }
    }

    public virtual async Task ActivateAsync(string id) {
        try {
            Guard.ParamIsNullOrEmpty(id, nameof(id));

            using var context = this.GetContext();
            var dbEntity = await GetAsync(id);
            if (dbEntity == null) {
                return;
            }
            dbEntity.IsActive = true;
            context.Update(dbEntity);
            await context.SaveChangesAsync();

        } catch (Exception ex) {
            throw DataProviderExceptionFactory.UpdateException<TEntity>(id, ex);
        }
    }

    public virtual async Task DeactivateAsync(string id) {
        try {
            Guard.ParamIsNullOrEmpty(id, nameof(id));

            using var context = this.GetContext();
            var dbEntity = await GetAsync(id);
            if (dbEntity == null) {
                return;
            }
            dbEntity.IsActive = false;
            context.Update(dbEntity);
            await context.SaveChangesAsync();

        } catch (Exception ex) {
            throw DataProviderExceptionFactory.UpdateException<TEntity>(id, ex);
        }
    }

    public virtual async Task DeleteAsync(string id) {
        try {
            Guard.ParamIsNullOrEmpty(id, nameof(id));

            using var context = this.GetContext();
            var dbEntity = await GetAsync(id);
            if (dbEntity == null) {
                return;
            }
            context.Remove(dbEntity);
            await context.SaveChangesAsync();

        } catch (Exception ex) {
            throw DataProviderExceptionFactory.DeleteException<TEntity>(id, ex);
        }
    }
    #endregion

    #region [ Public Methods - Get Single ]
    public virtual async Task<TEntity> GetAsync(string id) {
        try {
            Guard.ParamIsNullOrEmpty(id, nameof(id));

            using var context = this.GetContext();
            var dbResult = await context
                                            .Set<TEntity>()
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            return dbResult;

        } catch (Exception ex) {
            throw DataProviderExceptionFactory.GetSingleException<TEntity>(ex);
        }
    }
    #endregion

    #region [ Public Methods - Get List ]
    public virtual async Task<List<TEntity>> GetAllAsync() {
        try {
            using var context = this.GetContext();
            var dbResult = await context
                                        .Set<TEntity>()
                                        .AsNoTracking()
                                        .ToListAsync();
            return dbResult;

        } catch (Exception ex) {
            throw DataProviderExceptionFactory.GetListException<TEntity>(ex);
        }
    }

    public virtual async Task<List<TEntity>> GetActiveAsync() {
        try {
            using var context = this.GetContext();
            var dbResult = await context
                                            .Set<TEntity>()
                                            .AsNoTracking()
                                            .Where(x => x.IsActive == true)
                                            .ToListAsync();
            return dbResult;

        } catch (Exception ex) {
            throw DataProviderExceptionFactory.GetListException<TEntity>(ex);
        }
    }

    public virtual async Task<List<TEntity>> GetInActiveAsync() {
        try {
            using var context = this.GetContext();
            var dbrResult = await context
                                            .Set<TEntity>()
                                            .AsNoTracking()
                                            .Where(x => x.IsActive == false)
                                            .ToListAsync();
            return dbrResult;
        } catch (Exception ex) {
            throw DataProviderExceptionFactory.GetListException<TEntity>(ex);
        }
    }

    public virtual async Task<List<TEntity>> GetBatchAsync(List<string> entityIds) {
        try {
            Guard.PropertyIsNull(entityIds, nameof(entityIds));

            using var context = this.GetContext();
            var dbResult = await context
                                            .Set<TEntity>()
                                            .AsNoTracking()
                                            .Where(x => entityIds.Contains(x.Id))
                                            .ToListAsync();
            return dbResult;

        } catch (Exception ex) {
            throw DataProviderExceptionFactory.GetListException<TEntity>(ex);
        }
    }

    public virtual async Task<List<TEntity>> GetChangesAsync(DateTime date) {
        try {
            Guard.PropertyIsNull(date, nameof(date));

            using var context = this.GetContext();
            var dbResult = await context
                                            .Set<TEntity>()
                                            .AsNoTracking()
                                            .Where(x => x.UpdatedAt < date)
                                            .ToListAsync();
            return dbResult;

        } catch (Exception ex) {
            throw DataProviderExceptionFactory.GetListException<TEntity>(ex);
        }
    }
    #endregion

    #region [ Public Methods - Checks ]
    public async Task<bool> AnyAsync() {
        try {
            using var context = this.GetContext();
            var dbResult = await context
                                        .Set<TEntity>()
                                        .AsNoTracking()
                                        .AnyAsync();
            return dbResult;

        } catch (Exception ex) {
            throw DataProviderExceptionFactory.GetListException<TEntity>(ex);
        }
    }
    #endregion

    #region [ Private Methods ]
    protected TContext GetContext() {
        return this._dbContextFactory.CreateDbContext();
    }
    #endregion

    #region [ Obsolete Methods ]
    [Obsolete("Please stop using.")]
    public Task<List<TEntity>> GetListAsync(string queryText) {
        throw new NotImplementedException();
    }
    #endregion
}