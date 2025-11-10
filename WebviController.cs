using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using WebviAPI.IServices;
using WebviAPI.Model;
using WebviAPI.Services;
using static WebviAPI.Services.WebviService;

namespace WebviAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebviController : ControllerBase
    {
        private readonly WebviService _webviService;

        public WebviController(WebviService webviService)
        {
            _webviService = webviService;
        }

        [HttpPost("submit")]
        public IActionResult Submit([FromBody] ContantFrom form)
        {
            if (form == null)
                return BadRequest("Form data is missing.");

            try
            {
                _webviService.SendEmail(form);
                return Ok(new { success = true, message = "Email sent successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }

        }


    }
}

