using PTS.Core.Signatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Models.EmailParameter
{
    public class EmailParameterModel:IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SmtpServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
