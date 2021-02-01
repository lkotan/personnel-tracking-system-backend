using FluentValidation;
using PTS.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Business.Validations.Fluent
{
    public class ChangePasswordValidator:AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().NotNull();
            RuleFor(x => x.NewPassword).NotEmpty().NotNull();
        }
    }
}
