using System.Collections.Generic;

namespace Security.Application.Dto.Paginate
{
    public class PaginateResponseDto<TEntity>
    {
        public PaginateResponseDto()
        {
            Entities = new List<TEntity>();
        }
        public List<TEntity> Entities { get; set; }
        public int Count { get; set; }
    }
}
