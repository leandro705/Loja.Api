using Loja.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Api.Controllers
{
    [Route("api/email")]   
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _emailService.Send("", "", "");
            return Ok();
        }
    }
}
