using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PTS.API.Repositories;
using PTS.Business.Abstract;
using PTS.Models.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTS.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController: ControllerRepository<INotificationService, NotificationModel>
    {
        private readonly INotificationService _service;

        public NotificationsController(INotificationService service, IMapper mapper) : base(service, mapper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
    }
}
