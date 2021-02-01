using PTS.Core.Enums;
using PTS.Core.Signatures;

namespace PTS.Entities
{
    public class EmailTemplate:IBaseEntity
    {
        public int Id { get; set; }
        public int EmailParameterId { get; set; }
        public EmailTemplateType TemplateType { get; set; }
        public string Title { get; set; }
        public string MessageTemplate { get; set; }

        public EmailParameter EmailParameter { get; set; }
    }
}
