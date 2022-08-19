﻿using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using Security.Transversal.Common.TreeExpressions;

namespace Security.Application.MainModule.PrimeNG.Operators
{
    public class ContainsOperator : IOperator
    {
        public Expression GenerateCompareExpression<T>(ParameterExpression parameterExpression, string itemField,
            Expression expressionValue)
            where T : class
        {
            MethodCallExpression contains = null;
            MethodInfo stringContainsMethod = typeof(string).GetMethod("Contains", new[] {typeof(string)});
            var member = TreeExpressionHelper.GetMemberAccessLambda<T>(parameterExpression, itemField);

            if (expressionValue is ConstantExpression {Value: IList} constant && constant.Value.GetType().IsGenericType)
            {
                var type = constant.Value.GetType();
                var containsInfo = type.GetMethod("Contains", new[] { type.GetGenericArguments()[0] });
                contains = Expression.Call(constant, containsInfo, member);
            }

            return contains ?? Expression.Call(member, stringContainsMethod, expressionValue);
        }
    }
}