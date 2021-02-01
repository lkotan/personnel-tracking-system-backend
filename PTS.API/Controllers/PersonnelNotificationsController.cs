using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTS.API.Repositories;
using PTS.Business.Abstract;
using PTS.Models.PersonnelNotificaion;

namespace PTS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelNotificationsController : ControllerRepository<IPersonnelNotificationService,PersonnelNotificationModel>
    {
        private readonly IPersonnelNotificationService _service;

        public PersonnelNotificationsController(IPersonnelNotificationService service,IMapper mapper):base(service,mapper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int notificationId)
        {
            return Ok(await _service.GetAllAsync(notificationId));
        }

        [HttpGet("GetAllByPersonnelId")]
        public async Task<IActionResult> GetAllByPersonnelId([FromQuery] int personnelId)
        {
            return Ok(await _service.GetAllByPersonnelIdAsync(personnelId));
        }
        [HttpGet("GetAllMyNotification")]
        public async Task<IActionResult> GetAllMyNotification()
        {
            return Ok(await _service.GetAllMyNotificatioAsync());
        }

        [HttpPatch("IsRead")]
        public async Task<IActionResult> IsRead([FromQuery] int id)
        {
            return Ok(await _service.IsReadAsync(id));
        }
    }
}