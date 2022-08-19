using System.Linq;

namespace Security.Transversal.Common.Paginate.primeNG
{
    public class PrimeNGPaginateResponse<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> Entities { get; set; }
        public int TotalCount { get; set; }
    }
}
