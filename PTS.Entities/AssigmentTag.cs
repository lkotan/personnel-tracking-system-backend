using PTS.Core.Signatures;

namespace PTS.Entities
{
    public class AssigmentTag:IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TagColor { get; set; }
        public string TagBackgroundColor { get; set; }
    }
}
