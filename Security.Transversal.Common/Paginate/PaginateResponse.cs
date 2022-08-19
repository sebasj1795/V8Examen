using System.Collections.Generic;
using System.Linq;

namespace Security.Transversal.Common.Paginate
{
    //public class PaginateResponseDto<TEntity> where TEntity : class
    //{
    //    public PaginateResponseDto()
    //    {
    //        List = new List<TEntity>();
    //    }
    //    public List<TEntity> List { get; set; }
    //    public int TotalRows { get; set; }
    //}

    public class PaginateResponse<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> List { get; set; }
        public int TotalRows { get; set; }
    }

}
