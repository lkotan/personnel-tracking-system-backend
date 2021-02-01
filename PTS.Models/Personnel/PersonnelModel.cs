using PTS.Core.Enums;
using PTS.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Models.Personnel
{
    public class PersonnelModel:IBaseModel
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? TitleId { get; set; }
        public int? DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gsm { get; set; }
        public bool IsBlocked { get; set; }
        public PersonnelType PersonnelType { get; set; }
    }
}
