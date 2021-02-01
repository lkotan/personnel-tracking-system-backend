using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PTS.API.Repositories;
using PTS.Business.Abstract;
using PTS.Core.Enums;
using PTS.Core.Helpers;
using PTS.Models.EmailTemplate;

namespace PTS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplatesController : ControllerRepository<IEmailTemplateService,EmailTemplateModel>
    {
        private readonly IEmailTemplateService _service;

        public EmailTemplatesController(IEmailTemplateService service, IMapper mapper) : base(service, mapper)
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

        [HttpGet("EmailTemplateTypes")]
        public async Task<IActionResult> EmailTemplateTypes()
        {
            await Task.CompletedTask;
            return Ok(EnumHelper.List<EmailTemplateType>().OrderByDescending(x => x.Description));
        }
    }
}