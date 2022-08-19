using System;
using System.Collections.Generic;

namespace Security.Domain.Entities
{
    public partial class App
    {
        public App()
        {
            Modules = new HashSet<Module>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Server { get; set; }
        public string UserServer { get; set; }
        public string PasswordServer { get; set; }
        public string Port { get; set; }
        public string NameBd { get; set; }
        public int Platform { get; set; }
        public byte State { get; set; }
        public int UserCrea { get; set; }
        public DateTime DateCrea { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? DateUpd { get; set; }
        public int IdCompany { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
    }
}