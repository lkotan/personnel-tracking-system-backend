using FluentValidation;
using PTS.Models.PersonnelNotificaion;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Business.Validations.Fluent
{
    public class PersonnelNotificationValidator:AbstractValidator<PersonnelNotificationModel>
    {
        public PersonnelNotificationValidator()
        {
            RuleFor(x => x.NotificationId).GreaterThan(0);
            RuleFor(x => x.PersonnelId).GreaterThan(0);
        }
    }
}
