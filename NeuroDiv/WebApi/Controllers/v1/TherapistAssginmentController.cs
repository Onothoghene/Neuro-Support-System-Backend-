using Application.Features.TherapistAssignment.Command;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TherapistAssginmentController : BaseApiController
    {
        /// <summary>
        /// Assign a therapist to a child.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/therapists")]
        public async Task<IActionResult> AssignTherapist(Guid id, AssignTherapistCommand command)
        {
            command.ChildProfileId = id;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Remove a therapist from a child — sets EndDate on the assignment.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="therapistId"></param>
        /// <returns></returns>
        [HttpDelete("{id}/therapists/{therapistId}")]
        public async Task<IActionResult> RemoveTherapist(Guid id, Guid therapistId)
        {
            return Ok(await Mediator.Send(new RemoveTherapistAssignmentCommand
            {
                ChildProfileId = id,
                TherapistId = therapistId,
            }));
        }
    }
}