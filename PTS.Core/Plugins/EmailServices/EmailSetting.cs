namespace PTS.Core.Plugins.EmailServices
{
    public class EmailSetting
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailOptions EmailOptions { get; set; }
    }
}