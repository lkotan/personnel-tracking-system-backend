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
using PTS.Core.Helpers;
using PTS.Models.Assigment;

namespace PTS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigmentsController : ControllerRepository<IAssigmentService,AssigmentModel>
    {
        private readonly IAssigmentService _service;

        public AssigmentsController(IAssigmentService service,IMapper mapper):base(service,mapper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int? personnelId,[FromQuery]AssigmentStatus? statusId, [FromQuery] string keyword)
        {
            return Ok(await _service.GetAllAsync(personnelId, statusId, keyword));
        }
        [HttpGet("GetAllMyAssigment")]
        public async Task<IActionResult> GetAllMyAssigment()
        {
            return Ok(await _service.GetAllMyAssigmentAsync());
        }

        [HttpPatch("ChangePriority")]
        public async Task<IActionResult> ChangePriority([FromQuery] int assigmentId,[FromQuery] short priority)
        {
            return Ok(await _service.ChangePriorityAsync(assigmentId, priority));
        }

        [HttpGet("PriorityDegree")]
        public async Task<IActionResult> PriorityDegree()
        {
            await Task.CompletedTask;
            return Ok(EnumHelper.List<PriorityDegree>().OrderByDescending(x=>x.Id));
        }
        [HttpGet("Statuses")]
        public async Task<IActionResult> Statuses()
        {
            await Task.CompletedTask;
            return Ok(EnumHelper.List<AssigmentStatus>());
        }

        [HttpPatch("ChangeMyStatus")]
        public async Task<IActionResult> ChangeMyStatus([FromQuery] int assigmentId,AssigmentStatus status)
        {
            return Ok(await _service.ChangeMyStatusAsync(assigmentId,status));
        }
    }
}