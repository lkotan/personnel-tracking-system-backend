using FluentValidation;
using PTS.Models.AssigmentTag;

namespace PTS.Business.Validations.Fluent
{
    public class AssigmentTagValidator:AbstractValidator<AssigmentTagModel>
    {
        public AssigmentTagValidator()
        {
            RuleFor(x => x.Name).Length(3, 100);
            RuleFor(x => x.TagBackgroundColor).Length(1, 7);
            RuleFor(x => x.TagColor).Length(1, 7);
        }
    }
}
