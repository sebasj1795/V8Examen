using System;

namespace Security.Application.Dto.User
{
    public class UserListResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte State { get; set; }
        public bool Expire { get; set; }
        public DateTime? DateExpire { get; set; }
        public bool SuperUser { get; set; }
        public int IdRol { get; set; }
        public bool EmailConfirm { get; set; }
        public bool? ChangePassword { get; set; }
        public int NumberAttempt { get; set; }
        public DateTime? DateAttempt { get; set; }
    }
}
