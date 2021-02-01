using PTS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Core.Plugins.Authentication.Models
{
    public class UserInfo
    {
        public UserInfo()
        {
            Rules = new List<PersonnelRule>();
        }

        public int PersonnelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePhoto { get; set; }
        public PersonnelType PersonnelType { get; set; }
        public List<PersonnelRule> Rules { get; set; }
    }
}
