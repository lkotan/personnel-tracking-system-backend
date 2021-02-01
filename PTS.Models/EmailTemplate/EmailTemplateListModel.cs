using PTS.Core.Signatures;
namespace PTS.Models.EmailTemplate
{
    public class EmailTemplateListModel: IBaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
