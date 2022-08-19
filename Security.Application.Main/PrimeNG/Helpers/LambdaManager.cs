using LinqKit;
using Security.Application.MainModule.PrimeNG.Operators;
using Security.Transversal.Common.Constants;
using Security.Transversal.Common.Helpers;
using Security.Transversal.Common.TreeExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Security.Application.MainModule.PrimeNG.Helpers
{
    public static class LambdaManager
    {
        public static Expression<Func<T, bool>> ConvertToLambda<T>(List<ColumnsFilter> filters) where T : class
        {
            var parameterExpression = Expression.Parameter(typeof(T), "p");
            Expression<Func<T, bool>> expresionsLambdaSet = ReadFilterColumns<T>(filters, parameterExpression);

            return expresionsLambdaSet ?? PredicateBuilder.New<T>(true);
        }

        private static readonly Dictionary<string, IOperator> Operators = new()
        {
            { FilterMatchModePrimeNGConst.Contains, new ContainsOperator() },
            { FilterMatchModePrimeNGConst.In, new ContainsOperator() },
            { FilterMatchModePrimeNGConst.Equals, new EqualOperator() },
            { FilterMatchModePrimeNGConst.NotEquals, new NotEqualOperator() },
            { FilterMatchModePrimeNGConst.GreaterThanOrEqualTo, new GreaterThanOrEqualOperator() },
            { FilterMatchModePrimeNGConst.LessThanOrEqualTo, new LessThanOrEqualOperator() },
            { FilterMatchModePrimeNGConst.GreaterThan, new GreaterThanOperator() },
            { FilterMatchModePrimeNGConst.LessThan, new LessThanOperator() },
            { FilterMatchModePrimeNGConst.EndsWith, new EndsWithOperator() },
            { FilterMatchModePrimeNGConst.StartsWith, new StartsWithOperator() },
            { FilterMatchModePrimeNGConst.DateIs, new EqualDateOperator() },
            { FilterMatchModePrimeNGConst.DateIsNot, new NotEqualDateOperator() },
            { FilterMatchModePrimeNGConst.DateBefore, new LessThanDateOperator() },
            { FilterMatchModePrimeNGConst.DateAfter, new GreaterThanDateOperator() },
            { FilterMatchModePrimeNGConst.NotContains, new NotContainsOperator() }
        };

        private static Expression<Func<T, bool>> ReadFilterColumns<T>(List<ColumnsFilter> filters,
            ParameterExpression parameterExpression) where T : class
        {
            Expression<Func<T, bool>> expresionsLambdaSet = null;

            foreach (var filter in filters)
            {
                var pascalCaseField = StringHelper.ToPascalCase(filter.Field);
                var fieldType = TreeExpressionHelper.GetPropertyType<T>(pascalCaseField);
                var constantExpression = GetConstantExpression(filter.Value, fieldType);

                var comparisonFilterExpression = Operators[filter.Operator]
                    .GenerateCompareExpression<T>(parameterExpression, pascalCaseField, constantExpression);

                var expressionLambdaFilter =
                    Expression.Lambda<Func<T, bool>>(comparisonFilterExpression, parameterExpression);

                expresionsLambdaSet = expresionsLambdaSet == null
                    ? expressionLambdaFilter
                    : expresionsLambdaSet.And(expressionLambdaFilter);
            }

            return expresionsLambdaSet;
        }

        private static ConstantExpression GetConstantExpression(string value, Type type)
        {
            if (type == typeof(string)) return Expression.Constant(value, type);

            var nullableType = Nullable.GetUnderlyingType(type);

            if (nullableType == typeof(DateTime)) type = nullableType;

            return Expression.Constant(Convert.ChangeType(value, nullableType ?? type), type);
        }
    }
}
