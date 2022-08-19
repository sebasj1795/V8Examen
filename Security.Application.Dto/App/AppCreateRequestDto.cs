namespace Security.Application.Dto.App
{
    public class AppCreateRequestDto
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Server { get; set; }
        public string UserServer { get; set; }
        public string PasswordServer { get; set; }
        public string Port { get; set; }
        public string NameBd { get; set; }
        public int Platform { get; set; }
        public int IdCompany { get; set; }
    }
}
