
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Security.Transversal.Common.Paginate
{
    public class PaginateRequest<T> where T : class
    {
        //public Expression<Func<T, object>> ColumnOrder { get; set; }
        public string ColumnOrder { get; set; }
        public int Order { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public Expression<Func<T, bool>> WhereFilter { get; set; }
        public Func<IQueryable<T>, IIncludableQueryable<T, object>> Includes { get; set; }
    }

}
