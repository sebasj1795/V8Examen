using System.Linq.Expressions;

namespace Security.Application.MainModule.PrimeNG.Operators
{
    public interface IOperator
    {
        Expression GenerateCompareExpression<T>(ParameterExpression parameterExpression, string itemField,
            Expression expressionValue)
            where T : class;
    }
}