using System;

namespace Security.Transversal.Auth.Entity
{
    public class UserToken
    {
        public string Token { get; set; }
        public double ExpireIn { get; set; }
        public DateTime Expiration { get; set; }
    }
}
