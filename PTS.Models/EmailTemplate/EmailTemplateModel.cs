using PTS.Core.Enums;
using PTS.Core.Signatures;

namespace PTS.Models.EmailTemplate
{
    public class EmailTemplateModel: IBaseModel
    {
        public int Id { get; set; }
        public int EmailParameterId { get; set; }
        public EmailTemplateType TemplateType { get; set; }
        public string Title { get; set; }
        public string MessageTemplate { get; set; }
    }
}
