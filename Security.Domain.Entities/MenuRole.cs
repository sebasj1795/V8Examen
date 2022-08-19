using System;
using System.Collections.Generic;

namespace Security.Domain.Entities
{
    public partial class MenuRole
    {
        public int Id { get; set; }
        public int IdRole { get; set; }
        public int IdMenuAction { get; set; }
        public byte State { get; set; }
        public int UserCrea { get; set; }
        public DateTime DateCrea { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? DateUpd { get; set; }

        public virtual MenuAction MenuAction { get; set; }
        public virtual Role Role { get; set; }
    }
}