using FluentValidation;
using PTS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Business.Validations.Fluent
{
    public class RuleValidator : AbstractValidator<Rule>
    {
        public RuleValidator()
        {
            RuleFor(x => x.ApplicationModule).IsInEnum();
        }
    }
}
