using System.Linq.Expressions;
using Security.Transversal.Common.TreeExpressions;

namespace Security.Application.MainModule.PrimeNG.Operators
{
    public class EqualOperator : IOperator
    {
        public Expression GenerateCompareExpression<T>(ParameterExpression parameterExpression, string itemField,
            Expression expressionValue)
            where T : class
        {
            return Expression.Equal(TreeExpressionHelper.GetMemberAccessLambda<T>(parameterExpression, itemField),
                expressionValue);
        }
    }
}