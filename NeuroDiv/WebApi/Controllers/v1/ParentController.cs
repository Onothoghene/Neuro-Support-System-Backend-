using Application.Features.ParentProfile.Command;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ParentController : BaseApiController
    {
        /// <summary>
        /// Add or update a parent/guardian. Pass Id to update, omit to create
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id?}/parents")]
        public async Task<IActionResult> AddOrUpdateParent(Guid? id, AddOrUpdateParentProfileCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }
    }
}