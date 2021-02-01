using FluentValidation;
using PTS.Models.Role;

namespace PTS.Business.Validations.Fluent
{
    public class RoleValidator:AbstractValidator<RoleModel>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Description).Length(3, 100);
        }
    }
}
