using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Query;
using Security.Transversal.Common.Paginate;
using Security.Transversal.Common.Paginate.primeNG;

namespace Security.Domain.Interfaces.IRepository
{
    public interface IRepository<TEntity, in TId> where TEntity : class
    {
        Task<TEntity> GetAsync(TId id);

        Task<TEntity> GetAsync(TId id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        IQueryable<TEntity> All(bool @readonly = true);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            bool @readonly = true);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void RemoveRange(IEnumerable<TEntity> entities);

        void UpdateRange(IEnumerable<TEntity> entities);
        void Add(TEntity entity);

        Task<ValidationResult> AddAsync(TEntity entity, params IValidator<TEntity>[] validaciones);

        Task<ValidationResult> AddAsync(TEntity entity, IValidator<TEntity> validation);

        Task<ValidationResult> UpdateAsync(TEntity entity, params IValidator<TEntity>[] validaciones);
        void AddRange(IEnumerable<TEntity> entities);

        Task<TEntity> AddAsync(TEntity entity);

        Task<ValidationResult> UpdateAsync(TEntity entity, IValidator<TEntity> validation);

        Task<ValidationResult> DeleteAsync(TEntity entity, params IValidator<TEntity>[] validaciones);

        Task<ValidationResult> DeleteAsync(TEntity entity, IValidator<TEntity> validation);

        Task<ValidationResult> ValidateEntityAsync(TEntity entity, IValidator<TEntity> validation);

        Task<ValidationResult> ValidateEntityAsync(TEntity entity, IEnumerable<IValidator<TEntity>> validations);

        Task<IEnumerable<TDto>> RunSqlQuery<TDto>(string storeProcedure, object parameters = null);

        Task<string> StringResultProcedure(string storeProcedure, object parameters = null);

        Task<ValidationResult> AddEntityAsync(TEntity entity, ValidationResult validationResultEntity);

        Task ExecuteNonQueryAsync(string sqlQuery, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null);

        Task<PaginateResponse<TEntity>> FindAllPagingAsync(PaginateRequest<TEntity> parameters,
            bool @readonly = true);

        Task<PrimeNGPaginateResponse<TEntity>> FindAllPagingAsync(PrimeNGPaginateRequest<TEntity> parameters,
            bool @readonly = true);

    }
}
