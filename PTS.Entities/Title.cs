using PTS.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Entities
{
    public class Title:IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
