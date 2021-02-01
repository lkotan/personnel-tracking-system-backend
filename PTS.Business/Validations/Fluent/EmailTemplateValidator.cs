using FluentValidation;
using PTS.Models.EmailTemplate;

namespace PTS.Business.Validations.Fluent
{
    public class EmailTemplateValidator:AbstractValidator<EmailTemplateModel>
    {
        public EmailTemplateValidator()
        {
            RuleFor(x => x.Title).Length(1, 100);
            RuleFor(x => x.TemplateType).IsInEnum();
            RuleFor(x => x.EmailParameterId).GreaterThan(0);
        }
    }
}
