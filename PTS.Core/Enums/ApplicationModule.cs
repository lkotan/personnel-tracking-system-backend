using System.ComponentModel.DataAnnotations;

namespace PTS.Core.Enums
{
    public enum ApplicationModule
    {
        [Display(Name ="Null")] Null=0,
        [Display(Name ="Personeller")] Personnel=1,
        [Display(Name = "Personel Yetkileri")] Role=2,
        [Display(Name = "Personel Görevleri")] Assigment = 3,
        [Display(Name ="Departmanlar")] Department=4,
        [Display(Name ="Ünvanlar")] Title=5,
        [Display(Name = "Email Parametreleri")] EmailParameter = 6,
        [Display(Name = "Email Şablonları")] EmailTemplate = 7,
        [Display(Name = "Bildirimler")] Notification = 8,
        [Display(Name ="Görev Etiketleri")] AssigmentTag=9,
        [Display(Name ="Personel Bildirimleri")] PersonnelNotification=10,
    }
}
