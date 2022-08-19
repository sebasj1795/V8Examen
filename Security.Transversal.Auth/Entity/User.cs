using System;
using System.Security.Claims;

namespace Security.Transversal.Auth.Entity
{
    public class User
    {
        public User() { }

        public User(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal != null)
            {
                Id = Convert.ToInt32(claimsPrincipal.FindFirst("Id")?.Value);
                RolId = Convert.ToInt32(claimsPrincipal.FindFirst("RolId")?.Value);
                Name = claimsPrincipal.FindFirst("Name")?.Value;
                UserName = claimsPrincipal.FindFirst("Username")?.Value;
                CompanyId = Convert.ToInt32(claimsPrincipal.FindFirst("CompanyId")?.Value);
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int RolId { get; set; }
        public string Email { get; set; }
        public string Platform { get; set; }
        public int CompanyId { get; set; }
        public int AppId { get; set; }
    }
}
