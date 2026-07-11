using Application.Features.ContactUs;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [AllowAnonymous]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApplicationSupportController : BaseApiController
    {
        public ApplicationSupportController()
        {
        }

        [HttpPost("contact-us-mail")]
        [AllowAnonymous]
        public async Task<IActionResult> ContactUsMail(ContactUsMail command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("test-email-sending")]
        [AllowAnonymous]
        public async Task<IActionResult> TestEmail(TestMailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }

}
