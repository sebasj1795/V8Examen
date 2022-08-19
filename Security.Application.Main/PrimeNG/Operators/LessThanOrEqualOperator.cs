using System.Linq.Expressions;
using Security.Transversal.Common.TreeExpressions;

namespace Security.Application.MainModule.PrimeNG.Operators
{
    public class LessThanOrEqualOperator : IOperator
    {
        public Expression GenerateCompareExpression<T>(ParameterExpression parameterExpression, string itemField,
            Expression expressionValue)
            where T : class
        {
            return Expression.LessThanOrEqual(
                TreeExpressionHelper.GetMemberAccessLambda<T>(parameterExpression, itemField), expressionValue);
        }
    }
}