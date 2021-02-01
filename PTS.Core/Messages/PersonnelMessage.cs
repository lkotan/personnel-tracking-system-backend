namespace PTS.Core.Messages
{
    public static class PersonnelMessage
    {
        public static string PersonnelNotFound => "Personel Bulunamadı.";
        public static string PersonnelIsBlocked => "Personel Pasif Edilmiş.";
        public static string PersonnelIsExists => "Personel Kayıtlı.";
        public static string PasswordWrong => "Hatalı Şifre";
        public static string LoginSuccessful => "Giriş yapıldı.";
        public static string LoginSuccessfulByRefreshToken => "Giriş yapıldı.";
        public static string LogoutSuccessful => "Çıkış yapıldı.";
        public static string AuthenticationError => "Personel Girişi Yapın";
        public static string TokenNotFound => "Token Mevcut değil.";
        public static string TokenIsUsed => "Token zaten kullanılmış.";
        public static string TokenExpired => "Token zaman aşımına uğramış.";
        public static string PasswordChangeSuccessful => "Şifre başarıyla değiştirildi";
    }
}
