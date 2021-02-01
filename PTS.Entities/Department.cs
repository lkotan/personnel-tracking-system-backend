using PTS.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Entities
{
    public class Department:IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
