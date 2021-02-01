using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PTS.API.Repositories;
using PTS.Business.Abstract;
using PTS.Models.Title;

namespace PTS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitlesController : ControllerRepository<ITitleService,TitleModel>
    {
        private readonly ITitleService _service;
        public TitlesController(ITitleService service, IMapper mapper) : base(service, mapper)
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