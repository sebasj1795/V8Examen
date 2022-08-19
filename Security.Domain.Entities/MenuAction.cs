using System;
using System.Collections.Generic;

namespace Security.Domain.Entities
{
    public partial class MenuAction
    {
        public MenuAction()
        {
            MenuRoles = new HashSet<MenuRole>();
        }

        public int Id { get; set; }
        public int IdMenu { get; set; }
        public int IdAction { get; set; }
        public byte State { get; set; }
        public int UserCrea { get; set; }
        public DateTime DateCrea { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? DateUpd { get; set; }

        public virtual Action Action { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual ICollection<MenuRole> MenuRoles { get; set; }
    }
}