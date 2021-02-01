using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PTS.Core.Plugins.Authentication;
using PTS.Core.Repositories;
using PTS.Core.Utilities.IoC;
using PTS.Entities;
using PTS.Models.Personnel;
using PTS.Models.PersonnelNotificaion;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PTS.Core.Extenstions;
using PTS.Models.Notification;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;
using PTS.Core.Aspect.Security;
using System.IO;

namespace PTS.Business.Hubs
{
    [SecurityAspect]
    public class NotificationHub : Hub
    {
        private readonly IDataAccessRepository<PersonnelNotification> _dalPersonnelNotify;

        public NotificationHub(IDataAccessRepository<PersonnelNotification> dalPersonnelNotify)
        {
            _dalPersonnelNotify = dalPersonnelNotify;
        }

        public override Task OnConnectedAsync()
        {
            return Clients.Client(Context.ConnectionId).SendAsync("SetConnectionId", Context.ConnectionId);
        }

        public async Task SendNotifyToAll(string gName)
        {
            var entities = await _dalPersonnelNotify.TableNoTracking
                .Select(x => new PersonnelNotificationListModel
                {
                    Id = x.Id,
                    NotificationId = x.NotificationId,
                    CreatedAt = x.Notification.CreatedAt,
                    IsRead = x.IsRead,
                    NotificationMessage = x.Notification.Message,
                    NotificationTitle = x.Notification.Title,
                    PersonnelInfo = new PersonnelInfoModel
                    {
                        Id = x.PersonnelId,
                        FirstName = x.Personnel.FirstName,
                        LastName = x.Personnel.LastName,
                        ProfilePhoto = x.Personnel.ProfilePhoto
                    }
                }).ToListAsync();

            await Clients.Group(gName).SendAsync("ReceiveNotifyToAll", entities);
        }

        public async Task AddToGroupAll(string gName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gName);

            await Clients.Group(gName).SendAsync("AddGroupAll", $"{Context.ConnectionId} in add group {gName}");
        }
    }
}
