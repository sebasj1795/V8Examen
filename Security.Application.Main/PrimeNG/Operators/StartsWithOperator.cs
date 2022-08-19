using System.Linq.Expressions;
using Security.Transversal.Common.TreeExpressions;

namespace Security.Application.MainModule.PrimeNG.Operators
{
    public class StartsWithOperator : IOperator
    {
        public Expression GenerateCompareExpression<T>(ParameterExpression parameterExpression, string itemField,
            Expression expressionValue)
            where T : class
        {
            var startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

            return Expression.Call(TreeExpressionHelper.GetMemberAccessLambda<T>(parameterExpression, itemField),
                startsWithMethod, expressionValue);
        }
    }
}