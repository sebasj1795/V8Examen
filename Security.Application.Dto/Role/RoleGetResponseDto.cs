namespace Security.Application.Dto.Role
{
    public class RoleGetResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public byte State { get; set; }
    }
}
