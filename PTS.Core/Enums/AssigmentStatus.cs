using System.ComponentModel.DataAnnotations;

namespace PTS.Core.Enums
{
    public enum AssigmentStatus
    {
        [Display(Name = "Yeni Görev")] New = 1,
        [Display(Name = "Devam Ediyor")] Process = 2,
        [Display(Name = "Tamamlandı")] Complated = 3
    }
}
