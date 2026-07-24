using Application.Features.Users.Command;
using Application.Features.Users.Query;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class UserController : BaseApiController
    {
        [HttpGet("user/{id?}")]
        public async Task<IActionResult> GetUserById(Guid? id)
        {
            return Ok(await Mediator.Send(new GetUserProfileQuery { Id = id }));
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
