using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PTS.API.Repositories;
using PTS.Business.Abstract;
using PTS.Models.AssigmentTag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigmentTagsController:ControllerRepository<IAssigmentTagService,AssigmentTagModel>
    {
        private readonly IAssigmentTagService _service;

        public AssigmentTagsController(IAssigmentTagService service,IMapper mapper):base(service,mapper)
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
