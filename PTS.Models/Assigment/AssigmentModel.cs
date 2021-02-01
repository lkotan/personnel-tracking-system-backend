using PTS.Core.Signatures;
using System;

namespace PTS.Models.Assigment
{
    public class AssigmentModel:IBaseModel
    {
        public int Id { get; set; }
        public int PersonnelId { get; set; }
        public int? AssigmentTagId { get; set; }

        public string CreatedUser { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public short Priority { get; set; }
        public DateTime DueDate { get; set; }
    }
}
