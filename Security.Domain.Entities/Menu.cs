using System;
using System.Collections.Generic;

namespace Security.Domain.Entities
{
    public partial class Menu
    {
        public Menu()
        {
            MenuActions = new HashSet<MenuAction>();
        }

        public int Id { get; set; }
        public int? IdParent { get; set; }
        public int IdModule { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? Order { get; set; }
        public bool? IsForm { get; set; }
        public int Level { get; set; }
        public string IconCss { get; set; }
        public string IconImg { get; set; }
        public byte State { get; set; }
        public int UserCrea { get; set; }
        public DateTime DateCrea { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? DateUpd { get; set; }

        public virtual Module Module { get; set; }
        public virtual ICollection<MenuAction> MenuActions { get; set; }
    }
}