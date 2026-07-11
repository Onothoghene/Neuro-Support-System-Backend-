using Application.Features.PersonalDetails.Command.Update;
using Application.Features.PersonalDetails.Query.GetById;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonalDetailsController : BaseApiController
    {
        [HttpGet("user/{id?}")]
        public async Task<IActionResult> GetPersonalDetailsByUserId(Guid? id)
        {
            return Ok(await Mediator.Send(new GetPersonalDetailsByIdQuery { Id = id }));
        }

        [HttpGet("user/lite/{id?}")]
        public async Task<IActionResult> GetPersonalDetailsByUserIdLite(Guid? id)
        {
            return Ok(await Mediator.Send(new GetPersonalDetailsByIdLiteQuery { Id = id }));
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdatePersonalDetailsByUserId(UpdatePersonalDetailsByUserIdCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
