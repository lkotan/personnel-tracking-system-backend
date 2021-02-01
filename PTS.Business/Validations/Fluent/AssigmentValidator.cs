using FluentValidation;
using PTS.Core.Extenstions;
using PTS.Models.Assigment;

namespace PTS.Business.Validations.Fluent
{
    public class AssigmentValidator:AbstractValidator<AssigmentModel>
    {
        public AssigmentValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull();

            RuleFor(x => x.Priority.ToInt()).ExclusiveBetween(0, 6).WithMessage("Görev derecesi 1 ve 5 arasında olmalıdır.");
            RuleFor(x => x.Title).Length(5, 100);
        }
    }
}
