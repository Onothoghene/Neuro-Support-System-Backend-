using Application.Features.ChildProfile.Command;
using Application.Features.ChildProfile.Query;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ChildProfileController : BaseApiController
    {
        /// <summary>
        /// Get all children — filterable by org, therapist, diagnosis, status, name.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="therapistId"></param>
        /// <param name="diagnosisTypeId"></param>
        /// <param name="isActive"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? organizationId, [FromQuery] Guid? therapistId,
                                                [FromQuery] Guid? diagnosisTypeId, [FromQuery] bool? isActive,
                                                [FromQuery] string? searchTerm)
        {
            return Ok(await Mediator.Send(new GetChildrenQuery
            {
                OrganizationId = organizationId,
                TherapistId = therapistId,
                DiagnosisTypeId = diagnosisTypeId,
                IsActive = isActive,
                SearchTerm = searchTerm,
            }));
        }

        /// <summary>
        /// Get a specific child's full profile with goals, therapists, and parents.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetChildProfileQuery { Id = id }));
        }

        /// <summary>
        /// Create a new child profile.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddChildProfileCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update a child's profile details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateChildProfileCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

    }
}