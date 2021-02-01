using FluentValidation;
using PTS.Models.EmailParameter;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Business.Validations.Fluent
{
    public class EmailParameterValidator:AbstractValidator<EmailParameterModel>
    {
        public EmailParameterValidator()
        {
            RuleFor(x => x.Name).Length(1, 100);
            RuleFor(x => x.SmtpServer).Length(1, 255);
            RuleFor(x => x.UserName).Length(1, 100);
            RuleFor(x => x.Password).Length(1, 50);
        }

    }
}
