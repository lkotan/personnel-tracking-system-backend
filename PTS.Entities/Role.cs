using PTS.Core.Signatures;
using System.Collections.Generic;

namespace PTS.Entities
{
    public class Role:IBaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsBlocked { get; set; }
        public ICollection<Rule> Rules { get; set; }
    }
}
