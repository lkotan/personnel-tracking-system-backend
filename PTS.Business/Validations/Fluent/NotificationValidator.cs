using FluentValidation;
using PTS.Models.Notification;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Business.Validations.Fluent
{
    public class NotificationValidator:AbstractValidator<NotificationModel>
    {
        public NotificationValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull();
            RuleFor(x => x.Message).NotEmpty().NotNull();

            RuleFor(x => x.Title).Length(5, 100);
        }
    }
}
