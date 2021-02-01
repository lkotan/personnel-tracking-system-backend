using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTS.API.Repositories;
using PTS.Business.Abstract;
using PTS.Core.Enums;
using PTS.Core.Extenstions;
using PTS.Core.Helpers;
using PTS.Models.Personnel;

namespace PTS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelsController : ControllerRepository<IPersonnelService,PersonnelModel>
    {
        private readonly IPersonnelService _service;

        public PersonnelsController(IPersonnelService service, IMapper mapper) : base(service, mapper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("SelectList")]
        public async Task<IActionResult> SelectList()
        {
            return Ok(await _service.SelectListAsync());
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromQuery]string email)
        {
            return Ok(await _service.ForgotPasswordAsync(email));
        }
     
        [HttpGet("PersonnelTypes")]
        public async Task<IActionResult> PersonnelTypes()
        {
            await Task.CompletedTask;
            return Ok(EnumHelper.List<PersonnelType>());
        }

        [HttpPatch("IsBlockedChange")]
        public async Task<IActionResult> IsBlockedChange([FromQuery] int id)
        {
            return Ok(await _service.IsBlockedChangeAsync(id));
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            return Ok(await _service.SearchAsync(keyword));
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            return Ok(await _service.ProfileAsync());
        }

        [HttpGet("DashChart")]
        public async Task<IActionResult> DashChart()
        {
            return Ok(await _service.GetDashChartAsync());
        }
    }
}