using Application.Features.Session.Command;
using Application.Features.Session.Query;
using Asp.Versioning;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SessionController : BaseApiController
    {
        /// <summary>
        /// Get all sessions — filterable by org, therapist, child, status.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="therapistId"></param>
        /// <param name="childProfileId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        [HttpGet("class")]
        public async Task<IActionResult> GetAll([FromQuery] Guid? organizationId, [FromQuery] Guid? therapistId,
                                                [FromQuery] Guid? childProfileId, [FromQuery] bool? isActive)
        {
            return Ok(await Mediator.Send(new GetSessionClassesQuery
            {
                OrganizationId = organizationId,
                TherapistId = therapistId,
                ChildProfileId = childProfileId,
                IsActive = isActive,
            }));
        }

        /// <summary>
        /// Get a specific session class with upcoming occurrences.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("class/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetSessionClassQuery { Id = id }));
        }

        /// <summary>
        /// Create a new session — recurring or one-off.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("class")]
        public async Task<IActionResult> Create(CreateSessionClassCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update session — title, description, mode, active status, online details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("class/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateSessionClassCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        ///  Get occurrences — filterable by class, therapist, child, status, date range.
        /// </summary>
        /// <param name="sessionClassId"></param>
        /// <param name="therapistId"></param>
        /// <param name="childProfileId"></param>
        /// <param name="status"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpGet("occurrences")]
        public async Task<IActionResult> GetOccurrences([FromQuery] Guid? sessionClassId, [FromQuery] Guid? therapistId,
                                                        [FromQuery] Guid? childProfileId, [FromQuery] SessionStatus? status,
                                                        [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            return Ok(await Mediator.Send(new GetSessionOccurrencesQuery
            {
                SessionClassId = sessionClassId,
                TherapistId = therapistId,
                ChildProfileId = childProfileId,
                Status = status,
                FromDate = fromDate,
                ToDate = toDate,
            }));
        }

        /// <summary>
        /// Get a specific occurrence with full details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("occurrences/{id}")]
        public async Task<IActionResult> GetOccurrence(Guid id)
        {
            return Ok(await Mediator.Send(new GetSessionOccurrenceQuery { Id = id }));
        }

        /// <summary>
        /// Start a scheduled occurrence.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("occurrences/{id}/start")]
        public async Task<IActionResult> Start(Guid id)
        {
            return Ok(await Mediator.Send(new StartSessionOccurrenceCommand { Id = id }));
        }

        /// <summary>
        /// Complete an in-progress occurrence.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("occurrences/{id}/complete")]
        public async Task<IActionResult> Complete(Guid id, CompleteSessionOccurrenceCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Cancel a single occurrence — series continues unaffected.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("occurrences/{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id, CancelSessionOccurrenceCommand command)
        {
            command.OccurrenceId = id;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Mark a single occurrence as no-show.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("occurrences/{id}/no-show")]
        public async Task<IActionResult> MarkNoShow(Guid id, MarkNoShowCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Add or update session notes and goal progress for the child.
        /// Can be called during or after the session.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("occurrences/{id}/records")]
        public async Task<IActionResult> AddOrUpdateChildRecord(Guid id, AddOrUpdateChildSessionRecordCommand command)
        {
            command.SessionOccurrenceId = id;
            return Ok(await Mediator.Send(command));
        }

    }
}