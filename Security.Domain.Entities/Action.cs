using System;
using System.Collections.Generic;

namespace Security.Domain.Entities
{
    public partial class Action
    {
        public Action()
        {
            MenuActions = new HashSet<MenuAction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte State { get; set; }
        public int UserCrea { get; set; }
        public DateTime DateCrea { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? DateUpd { get; set; }

        public virtual ICollection<MenuAction> MenuActions { get; set; }
    }
}