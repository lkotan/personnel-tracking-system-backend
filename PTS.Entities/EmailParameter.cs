using PTS.Core.Signatures;

namespace PTS.Entities
{
    public class EmailParameter:IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SmtpServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
