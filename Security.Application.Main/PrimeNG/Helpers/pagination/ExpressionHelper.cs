using AutoMapper;
using LinqKit;
using Security.Application.Dto.Base.PrimeNG;
using Security.Transversal.Common.Helpers;
using Security.Transversal.Common.TreeExpressions;
using System;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Extensions.ExpressionMapping;
using Security.Transversal.Common.Paginate.primeNG;

namespace Security.Application.MainModule.PrimeNG.Helpers.pagination
{
    public static class ExpressionHelper
    {
        public static PrimeNGPaginateRequest<TEntity> ConvertToPaginationParameterDomain<TEntity, TDto>(
            this PaginationParametersDto<TDto> parameters, IMapper mapper)
            where TEntity : class
            where TDto : class
        {
            var expressionEntity = mapper.MapExpression<Expression<Func<TEntity, bool>>>(parameters.WhereFilter);

            IOderByExpression orderByLambda = null;

            if (!string.IsNullOrWhiteSpace(parameters.SortField))
            {
                var pascalCaseField = StringHelper.ToPascalCase(parameters.SortField);
                PropertyInfo propertyDtoOrderBy = typeof(TDto).GetProperty(pascalCaseField);
                var typeArguments = new[] { typeof(TDto), propertyDtoOrderBy.PropertyType };

                Type type = typeof(OrderByExpression<,>).MakeGenericType(typeArguments);

                orderByLambda = (IOderByExpression)Activator.CreateInstance(type,
                    TreeExpressionHelper.GetMemberAccessLambda<TDto>(pascalCaseField));
            }

            var paginationParameters = new PrimeNGPaginateRequest<TEntity>
            {
                SortField = orderByLambda?.GetLambdaExpression<TEntity>(mapper),
                AmountRows = parameters.AmountRows,
                Start = parameters.Start,
                SortType = parameters.SortType,
                WhereFilter = expressionEntity
            };

            return paginationParameters;
        }

        public static Expression<Func<TEntity, bool>> AddCondition<TEntity>(
            this Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, bool>> newRestriction)
        {
            if (predicate == null)
                return PredicateBuilder.New<TEntity>().And(newRestriction);

            return predicate.And(newRestriction);
        }
    }
}
