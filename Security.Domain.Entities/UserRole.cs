
using System;

namespace Security.Domain.Entities
{
    public partial class UserRole
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdRole { get; set; }
        public bool Default { get; set; }
        public byte State { get; set; }
        public int UserCrea { get; set; }
        public DateTime DateCrea { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? DateUpd { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}