using Security.Transversal.Common.Enum;
using System;
using System.Linq.Expressions;

namespace Security.Application.Dto.Base.PrimeNG
{
    public class PaginationParametersDto<T> where T : class
    {
        public string SortField { get; set; }
        public SortTypePrimeNGEnum SortType { get; set; }
        public int Start { get; set; }
        public int AmountRows { get; set; }
        public Expression<Func<T, bool>> WhereFilter { get; set; }
    }
}
