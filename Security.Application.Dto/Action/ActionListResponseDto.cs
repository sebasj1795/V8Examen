using System;

namespace Security.Application.Dto.Action
{
    public class ActionListResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte State { get; set; }
        public int UserCrea { get; set; }
        public DateTime DateCrea { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? DateUpd { get; set; }
    }
}
