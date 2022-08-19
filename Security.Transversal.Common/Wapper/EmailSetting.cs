namespace Security.Transversal.Common.Wapper
{
    public  class EmailSetting
    {
        public string Address { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
