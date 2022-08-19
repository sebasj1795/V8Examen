namespace Security.Application.Dto.MasterDet
{
    public class MasterDetUpdateRequestDto
    {
        public int Id { get; set; }
        public int IdMaster { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Value2 { get; set; }
        public byte State { get; set; }
    }
}
