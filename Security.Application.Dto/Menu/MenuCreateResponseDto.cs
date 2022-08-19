namespace Security.Application.Dto.Menu
{
    public class MenuCreateResponseDto
    {
        public int Id { get; set; }
        public int? IdParent { get; set; }
        public int IdModule { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? Order { get; set; }
        public bool? IsForm { get; set; }
        public string IconCss { get; set; }
        public string IconImg { get; set; }
        public byte State { get; set; }
    }
}
