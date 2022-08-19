using AutoMapper;
using System.Linq.Expressions;
namespace Security.Application.Dto.Base.PrimeNG
{
    public interface IOderByExpression
    {
        LambdaExpression GetLambdaExpression<TEntity>(IMapper mapper);
    }
}
