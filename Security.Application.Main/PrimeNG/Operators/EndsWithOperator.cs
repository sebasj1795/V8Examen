using System.Linq.Expressions;
using Security.Transversal.Common.TreeExpressions;

namespace Security.Application.MainModule.PrimeNG.Operators
{
    public class EndsWithOperator : IOperator
    {
        public Expression GenerateCompareExpression<T>(ParameterExpression parameterExpression, string itemField,
            Expression expressionValue)
            where T : class
        {
            var endsWithMethod = typeof(string).GetMethod("EndsWith", new[] {typeof(string)});

            return Expression.Call(TreeExpressionHelper.GetMemberAccessLambda<T>(parameterExpression, itemField),
                endsWithMethod, expressionValue);
        }
    }
}