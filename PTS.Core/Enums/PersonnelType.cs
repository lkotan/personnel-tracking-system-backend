using System.ComponentModel.DataAnnotations;

namespace PTS.Core.Enums
{
    public enum PersonnelType
    {
        [Display(Name ="Personel")] Personnel=1,
        [Display(Name ="Admin")] Admin=20,
    }
}
