using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using PTS.Core.Utilities.Results.Result;

namespace PTS.Core.Plugins.EmailServices
{
    public interface IEmailService
    {
        Task<IResponse> SendMailAsync(string email, EmailSetting template, List<string> cc = null);
    }
}