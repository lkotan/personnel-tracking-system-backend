using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Core.Messages
{
    public static class DbMessage
    {
        public static string DataInserted => "Kayıt Eklendi";
        public static string DataNotFound => "Kayıt bulunamadı";
        public static string DataUpdated => "Güncellendi";
        public static string DataRemoved => "Silindi";
        public static string ObsoleteMethod => "Bu method artık kullanılmıyor.";
    }
}
