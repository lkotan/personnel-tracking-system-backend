using PTS.Core.Enums;
using PTS.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Entities
{
    public class Personnel:IBaseEntity
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? TitleId { get; set; }
        public int? DepartmentId { get; set; }
        public PersonnelType PersonnelType { get; set; } = PersonnelType.Personnel;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gsm { get; set; }
        public string ProfilePhoto { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiredDate { get; set; }
        public bool IsBlocked { get; set; }
        public Role Role { get; set; }
        public Title Title { get; set; }
        public Department Department { get; set; }
        public ICollection<PersonnelNotification> PersonnelNotifications { get; set; }
        public ICollection<Assigment> Assigments { get; set; }
    }
}
