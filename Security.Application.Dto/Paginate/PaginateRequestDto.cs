namespace Security.Application.Dto.Paginate
{
    public class PaginateRequestDto
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string ColumnOrder { get; set; }
        public int Order { get; set; }
    }
}
