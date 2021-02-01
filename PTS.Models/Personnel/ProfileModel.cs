using PTS.Core.Enums;
using PTS.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Models.Personnel
{
    public class ProfileModel:IBaseModel
    {
        public int Id { get; set; }
        public PersonnelType PersonnelType { get; set; }
        public string PersonnelTypeName { get; set; }
        public string Email { get; set; }
        public string Gsm { get; set; }
        public bool IsBlocked { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public PersonnelInfoModel PersonnelInfo { get; set; }
        public int AssigmentCount { get; set; }
    }
}
