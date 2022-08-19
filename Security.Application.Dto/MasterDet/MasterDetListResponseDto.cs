using System;

namespace Security.Application.Dto.MasterDet
{
    public class MasterDetListResponseDto
    {
        public int Id { get; set; }
        public int IdMaster { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public byte State { get; set; }
        public int UserCrea { get; set; }
        public DateTime DateCrea { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? DateUpd { get; set; }
    }
}
