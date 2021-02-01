using PTS.Core.Enums;

namespace PTS.Core.Models
{
    public class PersonnelRulesModel
    {
        public ApplicationModule ApplicationModule { get; set; }
        public string ApplicationModuleName { get; set; }
        public bool View { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}
