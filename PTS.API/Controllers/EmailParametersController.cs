using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTS.API.Repositories;
using PTS.Business.Abstract;
using PTS.Models.EmailParameter;

namespace PTS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailParametersController : ControllerRepository<IEmailParameterService,EmailParameterModel>
    {
        private readonly IEmailParameterService _service;

        public EmailParametersController(IEmailParameterService service, IMapper mapper) : base(service, mapper)
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
    }
}