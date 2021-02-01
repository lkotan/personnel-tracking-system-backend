using FluentValidation;
using PTS.Models.Personnel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Business.Validations.Fluent
{
    public class PersonnelValidator:AbstractValidator<PersonnelModel>
    {
        public PersonnelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull();
            RuleFor(x => x.LastName).NotEmpty().NotNull();
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();

            RuleFor(x => x.PersonnelType).IsInEnum();
            RuleFor(x => x.Gsm).NotNull();
        }
    }
}
