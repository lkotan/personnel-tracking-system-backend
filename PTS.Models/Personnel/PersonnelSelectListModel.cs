using PTS.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Models.Personnel
{
    public class PersonnelSelectListModel:IBaseModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ProfilePhoto { get; set; }
    }
}
