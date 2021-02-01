using System.ComponentModel.DataAnnotations;

namespace PTS.Core.Enums
{
    public enum PriorityDegree
    {
        [Display(Name = "Çok Düşük")] VeryLow = 1,
        [Display(Name = "Düşük")] Low = 2,
        [Display(Name = "Öncelikli")] Preferential = 3,
        [Display(Name = "Yüksek Öncelikli")] HighPriority = 4,
        [Display(Name ="Kritik")] Critical=5
    }
}
