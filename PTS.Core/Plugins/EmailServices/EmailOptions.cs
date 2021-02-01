namespace PTS.Core.Plugins.EmailServices
{
    public class EmailOptions
    {
        public string SmtpServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
