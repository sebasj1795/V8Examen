using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Security.Infrastructure.DataModel.Context;
using Security.Infrastucture.Repository.Extensions;
using Security.Domain.Interfaces.IRepository;
using Security.Transversal.Common.Paginate;
using Security.Transversal.Common.Paginate.primeNG;
using Security.Transversal.Common.Enum;

namespace Security.Infrastucture.Repository
{
    public class GenericRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
        where TId : IComparable<TId>
    {
        //public MainContext RepositoryContext { get; set; }
        protected MainContext MainContext { get; }
        protected DbSet<TEntity> DbSet { get; set; }

        public GenericRepository(MainContext mainContext)
        {
            MainContext = mainContext;
            DbSet = mainContext.Set<TEntity>();
            //RepositoryContext = mainContext;
        }

        public GenericRepository(MainContext mainContext, DbSet<TEntity> dbSet)
        {
            MainContext = mainContext;
            DbSet = dbSet;
        }

       

        public virtual IQueryable<TEntity> All(bool @readonly = true)
        {
            return @readonly ? DbSet.AsNoTracking() : DbSet;
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.Where(expression).CountAsync();
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            bool @readonly = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (@readonly)
                query = query.AsNoTracking();

            return query.Where(predicate);
        }

        public async Task<TEntity> GetAsync(TId id)
        {
            var keyProperty = MainContext.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties[0];
            var keyId = (int)Convert.ChangeType(id, typeof(int));

            var dbSet = (IQueryable<TEntity>)DbSet;

            return await dbSet.FirstOrDefaultAsync(x => EF.Property<int>(x, keyProperty.Name) == keyId);
        }

        public async Task<TEntity> GetAsync(TId id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var keyProperty = MainContext.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties[0];
            var keyId = (int)Convert.ChangeType(id, typeof(int));

            var dbSet = include != null ? include(DbSet) : (IQueryable<TEntity>)DbSet;

            return await dbSet.FirstOrDefaultAsync(x => EF.Property<int>(x, keyProperty.Name) == keyId);
        }

        public void Add(TEntity entity)
        {
            MainContext.Add<TEntity>(entity);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            MainContext.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public async Task<ValidationResult> AddAsync(TEntity entity, params IValidator<TEntity>[] validaciones)
        {
            var validateEntityResult = await ValidateEntityAsync(entity, validaciones);
            if (validateEntityResult.IsValid)
            {
                await MainContext.AddAsync(entity);
                MainContext.Entry(entity).State = EntityState.Added;
            }
            return validateEntityResult;
        }

        public async Task<ValidationResult> AddAsync(TEntity entity, IValidator<TEntity> validation)
        {
            var validateEntityResult = await ValidateEntityAsync(entity, validation);

            if (validateEntityResult.IsValid) {
                await MainContext.AddAsync(entity);
                MainContext.Entry(entity).State = EntityState.Added;
            }
            return validateEntityResult;
        }

        public async Task<ValidationResult> UpdateAsync(TEntity entity, params IValidator<TEntity>[] validaciones)
        {
            var validateEntityResult = await ValidateEntityAsync(entity, validaciones);

            if (validateEntityResult.IsValid)
                MainContext.Update(entity);

            return validateEntityResult;
        }

        public async Task<ValidationResult> UpdateAsync(TEntity entity, IValidator<TEntity> validation)
        {
            var validateEntityResult = await ValidateEntityAsync(entity, validation);

            if (validateEntityResult.IsValid) MainContext.Update(entity);

            return validateEntityResult;
        }

        public async Task<ValidationResult> DeleteAsync(TEntity entity, params IValidator<TEntity>[] validaciones)
        {
            var validateEntityResult = await ValidateEntityAsync(entity, validaciones);

            if (validateEntityResult.IsValid) MainContext.Remove(entity);

            return validateEntityResult;
        }

        public async Task<ValidationResult> DeleteAsync(TEntity entity, IValidator<TEntity> validation)
        {
            var validateEntityResult = await ValidateEntityAsync(entity, validation);

            if (validateEntityResult.IsValid) MainContext.Remove(entity);
            return validateEntityResult;
        }

        public async Task<ValidationResult> ValidateEntityAsync(TEntity entity, IValidator<TEntity> validation)
        {
            if (validation != null)
            {
                var validationResultEntity = await validation.ValidateAsync(entity);
                return validationResultEntity;
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> ValidateEntityAsync(TEntity entity, IEnumerable<IValidator<TEntity>> validations)
        {
            if (validations != null)
            {
                var errors = new List<ValidationFailure>();

                foreach (var validation in validations)
                {
                    var currentValidationResult = await validation.ValidateAsync(entity);

                    if (!currentValidationResult.IsValid)
                        errors.AddRange(currentValidationResult.Errors);
                }

                return new ValidationResult(errors);
            }

            return new ValidationResult();
        }

        public async Task<ValidationResult> AddEntityAsync(TEntity entity, ValidationResult validationResultEntity)
        {
            if (validationResultEntity.IsValid)
                //await DbSet.AddAsync(entity);
                await MainContext.AddAsync(entity);

            return validationResultEntity;
        }

        public async Task<IEnumerable<TDto>> RunSqlQuery<TDto>(string sqlQuery, object parameters = null)
        {
            return await MainContext.FromSqlQueryAsync<TDto>(sqlQuery, parameters);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public async Task ExecuteNonQueryAsync(string sqlQuery, object parameters = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            await MainContext.ExecuteNonQueryAsync(sqlQuery, parameters, transaction, commandTimeout);
        }

        public async Task<string> StringResultProcedure(string sqlQuery, object parameters = null)
        {
            return await MainContext.StringResultProcedure(sqlQuery, parameters);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool @readonly = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (@readonly)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            return query.Where(predicate);
        }

        public async Task<IEnumerable<TDto>> RunStoredProcedure<TDto>(string storeProcedure, object parameteres = null)
        {
            return await MainContext.FromSqlQueryAsync<TDto>(storeProcedure, parameteres);
        }

        public virtual void Update(TEntity entity)
        {
            if (DbSet.Local.All(p => p != entity))
            {
                DbSet.Attach(entity);
                var entry = MainContext.Entry(entity);
                entry.State = EntityState.Modified;
            }
        }

        public void Delete(TEntity entity)
        {
            DbSet.Attach(entity);

            var entry = MainContext.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        #region Paginate normal
        public virtual async Task<PaginateResponse<TEntity>> FindAllPagingAsync(
           PaginateRequest<TEntity> parameters, bool @readonly = true)
        {
            var paging = GetDataWithFilter(parameters, @readonly);
            return await PagingAsync(parameters, paging);
        }

        protected IQueryable<TEntity> GetDataWithFilter(PaginateRequest<TEntity> parameters, bool @readonly = true)
        {
            var dbSet = parameters.Includes != null ? parameters.Includes(DbSet) : (IQueryable<TEntity>)DbSet;
            if (parameters.WhereFilter is null) {
                return (@readonly ? dbSet.AsNoTracking() : dbSet);
            }
            return (@readonly ? dbSet.AsNoTracking() : dbSet).Where(parameters.WhereFilter);
        }

        protected async Task<PaginateResponse<TNewEntity>> PagingAsync<TNewEntity>(
            PaginateRequest<TNewEntity> parameters, IQueryable<TNewEntity> pagingQuery) where TNewEntity : class
        {
            if (!string.IsNullOrEmpty(parameters.ColumnOrder) && parameters.Order!=0)
            {
                string typeOrder = parameters.Order == -1 ? "desc" : "asc";
                pagingQuery = pagingQuery.OrderBy(parameters.ColumnOrder + " " + typeOrder);
            }

            return new PaginateResponse<TNewEntity>
            {
                TotalRows = await pagingQuery.CountAsync(),
                List = pagingQuery.Skip(parameters.Page).Take(parameters.Size)
            };
        }
        #endregion

        #region Paginate PrimeNG
        public virtual async Task<PrimeNGPaginateResponse<TEntity>> FindAllPagingAsync(
           PrimeNGPaginateRequest<TEntity> parameters, bool @readonly = true)
        {
            var paging = GetDataWithFilter(parameters, @readonly);
            return await PagingAsync(parameters, paging);
        }

        protected IQueryable<TEntity> GetDataWithFilter(PrimeNGPaginateRequest<TEntity> parameters, bool @readonly = true)
        {
            var dbSet = parameters.Includes != null ? parameters.Includes(DbSet) : (IQueryable<TEntity>)DbSet;
            if (parameters.WhereFilter is null)
            {
                return (@readonly ? dbSet.AsNoTracking() : dbSet);
            }
            return (@readonly ? dbSet.AsNoTracking() : dbSet).Where(parameters.WhereFilter);
        }

        protected async Task<PrimeNGPaginateResponse<TNewEntity>> PagingAsync<TNewEntity>(
            PrimeNGPaginateRequest<TNewEntity> parameters, IQueryable<TNewEntity> pagingQuery) where TNewEntity : class
        {
            if (parameters.SortField != null)
            {
                pagingQuery = parameters.SortType == SortTypePrimeNGEnum.Ascending
                    ? Queryable.OrderBy(pagingQuery, (dynamic)parameters.SortField)
                    : Queryable.OrderByDescending(pagingQuery, (dynamic)parameters.SortField);
            }

            return new PrimeNGPaginateResponse<TNewEntity>
            {
                TotalCount = await pagingQuery.CountAsync(),
                Entities = pagingQuery.Skip(parameters.Start).Take(parameters.AmountRows)
            };
        }
        #endregion
    }
}
