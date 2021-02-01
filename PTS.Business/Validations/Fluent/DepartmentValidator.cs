using FluentValidation;
using PTS.Models.Department;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Business.Validations.Fluent
{
    public class DepartmentValidator:AbstractValidator<DepartmentModel>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();

            RuleFor(x => x.Name).Length(3, 30).WithMessage("Departman adı alanı en az 3 en fazla 30 karakter olmalıdır.");
        }
    }
}
