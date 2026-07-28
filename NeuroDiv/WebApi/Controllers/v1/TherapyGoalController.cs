using Application.Features.TherapyGoal.Command;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TherapyGoalController : BaseApiController
    {
        /// <summary>
        /// Add or update a therapy goal. Pass Id to update, omit to create.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/goals")]
        public async Task<IActionResult> AddOrUpdateGoal(Guid id, AddOrUpdateTherapyGoalCommand command)
        {
            command.ChildProfileId = id;
            return Ok(await Mediator.Send(command));
        }

    }
}