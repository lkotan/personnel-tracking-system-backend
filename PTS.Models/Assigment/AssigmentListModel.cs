using PTS.Core.Enums;
using PTS.Core.Signatures;
using PTS.Models.AssigmentTag;
using PTS.Models.Personnel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Models.Assigment
{
    public class AssigmentListModel:IBaseModel
    {
        public int Id { get; set; }
        public AssigmentStatus Status { get; set; }
        public string StatusName { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public short Priority { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }

        public AssigmentTagModel TagInfo { get; set; }
        public PersonnelInfoModel PersonnelInfo { get; set; }

    }
}
