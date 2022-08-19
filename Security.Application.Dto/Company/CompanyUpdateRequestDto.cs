namespace Security.Application.Dto.Company
{
    public class CompanyUpdateRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ruc { get; set; }
        public byte State { get; set; }
    }
}
