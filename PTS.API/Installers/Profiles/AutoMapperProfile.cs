using AutoMapper;
using PTS.Entities;
using PTS.Models.Assigment;
using PTS.Models.AssigmentTag;
using PTS.Models.Department;
using PTS.Models.EmailParameter;
using PTS.Models.EmailTemplate;
using PTS.Models.Notification;
using PTS.Models.Personnel;
using PTS.Models.PersonnelNotificaion;
using PTS.Models.Role;
using PTS.Models.Rule;
using PTS.Models.Title;

namespace PTS.API.Installers.Profiles
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Title, TitleModel>();
            CreateMap<TitleModel, Title>();

            CreateMap<Department, DepartmentModel>();
            CreateMap<DepartmentModel, Department>();

            CreateMap<Personnel, PersonnelModel>();
            CreateMap<PersonnelModel, Personnel>();


            CreateMap<EmailParameter, EmailParameterModel>();
            CreateMap<EmailParameterModel, EmailParameter>();

            CreateMap<EmailTemplate, EmailTemplateModel>();
            CreateMap<EmailTemplateModel, EmailTemplate>();

            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();

            CreateMap<Rule, RuleModel>();
            CreateMap<RuleModel, Rule>();

            CreateMap<Rule, RuleListModel>();
            CreateMap<RuleListModel, Rule>();

            CreateMap<AssigmentTag, AssigmentTagModel>();
            CreateMap<AssigmentTagModel, AssigmentTag>();

            CreateMap<Assigment, AssigmentModel>();
            CreateMap<AssigmentModel, Assigment>();

            CreateMap<Assigment, AssigmentListModel>();
            CreateMap<AssigmentListModel, Assigment>();

            CreateMap<Notification, NotificationModel>();
            CreateMap<NotificationModel, Notification>();

            CreateMap<Notification, NotificationListModel>();
            CreateMap<NotificationListModel, Notification>();

            CreateMap<PersonnelNotification, PersonnelNotificationModel>();
            CreateMap<PersonnelNotificationModel, PersonnelNotification>();
        }
    }
}
