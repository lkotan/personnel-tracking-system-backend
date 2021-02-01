using PTS.Core.Enums;

namespace PTS.Core.Plugins.Authentication.Models
{
    public class PersonnelRule
    {
        public ApplicationModule ApplicationModule { get; set; }
        public bool View { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}
