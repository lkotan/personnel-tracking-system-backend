using PTS.Core.Signatures;

namespace PTS.Models.Role
{
    public class RoleModel:IBaseModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsBlocked { get; set; }
    }
}
