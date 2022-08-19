using System;

namespace Security.Application.Dto.User
{
    public class UserCreateResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Expire { get; set; }
        public DateTime? DateExpire { get; set; }
        public bool SuperUser { get; set; }
        public int IdRol { get; set; }
    }
}
