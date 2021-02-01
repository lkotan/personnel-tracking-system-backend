using PTS.Core.Signatures;

namespace PTS.Models.AssigmentTag
{
    public class AssigmentTagModel:IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TagColor { get; set; }
        public string TagBackgroundColor { get; set; }
    }
}
