using PTS.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Models.Department
{
    public class DepartmentModel:IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
