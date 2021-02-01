using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Models.Auth
{
    public class ChangePasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
