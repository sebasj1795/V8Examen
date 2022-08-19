using Microsoft.EntityFrameworkCore.Query;
using Security.Transversal.Common.Enum;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Security.Transversal.Common.Paginate.primeNG
{
    public class PrimeNGPaginateRequest<T> where T : class
    {
        public LambdaExpression SortField { get; set; }
        //public string SortField { get; set; }
        public SortTypePrimeNGEnum SortType { get; set; }
        public int Start { get; set; }
        public int AmountRows { get; set; }
        public bool IgnorePagination { get; set; }
        public Expression<Func<T, bool>> WhereFilter { get; set; }
        public Func<IQueryable<T>, IIncludableQueryable<T, object>> Includes { get; set; }
    }
}
