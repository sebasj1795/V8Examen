using System;
using System.Collections.Generic;

namespace Security.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public byte State { get; set; }
        public bool Expire { get; set; }
        public DateTime? DateExpire { get; set; }
        public bool SuperUser { get; set; }
        public bool EmailConfirm { get; set; }
        public bool? ChangePassword { get; set; }
        public int? NumberAttempt { get; set; }
        public DateTime? DateAttempt { get; set; }
        public byte ModeAuthentication { get; set; }
        public int UserCrea { get; set; }
        public DateTime DateCrea { get; set; }
        public int? UserUpd { get; set; }
        public DateTime? DateUpd { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}