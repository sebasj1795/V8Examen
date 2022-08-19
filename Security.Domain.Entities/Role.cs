using System;
using System.Collections.Generic;

namespace Security.Domain.Entities
{
    public partial class Role
    {
        public Role()
        {
            MenuRoles = new HashSet<MenuRole>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public byte State { get; set; }
        public int UserCrea { get; set; }
        public DateTime DateCrea { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? DateUpd { get; set; }

        public virtual ICollection<MenuRole> MenuRoles { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}