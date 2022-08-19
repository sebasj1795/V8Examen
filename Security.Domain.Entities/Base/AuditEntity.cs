using System;

namespace Security.Domain.Entities.Base
{
    public class AuditEntity<T> : Entity<T>
    {
        public int UserCrea { get; set; }
        public DateTime DateCrea { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? DateUpd { get; set; }
    }
}
