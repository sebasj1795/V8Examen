using System.Linq.Expressions;
using Security.Transversal.Common.TreeExpressions;

namespace Security.Application.MainModule.PrimeNG.Operators
{
    public class LessThanOperator : IOperator
    {
        public Expression GenerateCompareExpression<T>(ParameterExpression parameterExpression, string itemField,
            Expression expressionValue)
            where T : class
        {
            return Expression.LessThan(
                TreeExpressionHelper.GetMemberAccessLambda<T>(parameterExpression, itemField), expressionValue);
        }
    }
}