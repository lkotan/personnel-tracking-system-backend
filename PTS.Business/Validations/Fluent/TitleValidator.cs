using FluentValidation;
using PTS.Models.Title;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Business.Validations.Fluent
{
    public class TitleValidator:AbstractValidator<TitleModel>
    {
        public TitleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();

            RuleFor(x => x.Name).Length(3, 30).WithMessage("Ünvan adı alanı en az 3 en fazla 30 karakter olmalıdır.");
        }
    }
}
