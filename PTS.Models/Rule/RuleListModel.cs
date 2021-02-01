using PTS.Core.Enums;
using PTS.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Models.Rule
{
    public class RuleListModel:IBaseModel
    {
        public int Id { get; set; }
        public ApplicationModule ApplicationModule { get; set; }
        public string ModuleName { get; set; }
        public bool View { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}
