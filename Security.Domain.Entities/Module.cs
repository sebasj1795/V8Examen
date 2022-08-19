using System;
using System.Collections.Generic;

namespace Security.Domain.Entities
{
    public partial class Module
    {
        public Module()
        {
            Menus = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public int IdApp { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public string IconCss { get; set; }
        public string IconImg { get; set; }
        public byte State { get; set; }
        public int UserCrea { get; set; }
        public DateTime DateCrea { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? DateUpd { get; set; }

        public virtual App App { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}