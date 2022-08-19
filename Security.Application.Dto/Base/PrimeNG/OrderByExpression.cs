using AutoMapper;
using System;
using System.Linq.Expressions;
using AutoMapper.Extensions.ExpressionMapping;
namespace Security.Application.Dto.Base.PrimeNG
{
    public class OrderByExpression<TDto, TProperty> : IOderByExpression
    {
        private readonly Expression<Func<TDto, TProperty>> _sort;

        public OrderByExpression(Expression<Func<TDto, TProperty>> sort)
        {
            _sort = sort;
        }

        public LambdaExpression GetLambdaExpression<TEntity>(IMapper mapper)
        {
            return mapper.MapExpression<Expression<Func<TEntity, TProperty>>>(_sort);
        }
    }
}
