using System;
using System.Collections.Generic;

namespace Security.Domain.Entities
{
    public partial class Company
    {
        public Company()
        {
            Apps = new HashSet<App>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Ruc { get; set; }
        public byte State { get; set; }
        public int UserCrea { get; set; }
        public DateTime DateCrea { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? DateUpd { get; set; }

        public virtual ICollection<App> Apps { get; set; }
    }
}