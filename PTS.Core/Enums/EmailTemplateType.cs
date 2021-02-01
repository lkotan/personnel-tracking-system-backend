using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PTS.Core.Enums
{
    public enum EmailTemplateType
    {
        [Display(Name = "Personel Oluştuğunda Şifre Bildirimi")] CreatePersonnelPasswordNotification = 1,
        [Display(Name = "Şifre Unuttum")] ForgotPassword = 2,
    }
}
