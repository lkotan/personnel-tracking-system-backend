using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTS.API.Repositories;
using PTS.Business.Abstract;
using PTS.Models.Rule;

namespace PTS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : ControllerRepository<IRuleService, RuleModel>
    {
        private readonly IRuleService _service;

        public RulesController(IRuleService service, IMapper mapper) : base(service, mapper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int roleId)
        {
            return Ok(await _service.GetAllAsync(roleId));
        }

        [HttpPost("SaveRange")]
        public async Task<IActionResult> SaveRange([FromBody] IEnumerable<RuleModel> models)
        {
            return Ok(await _service.SaveRangeAsync(models));
        }
    }
}