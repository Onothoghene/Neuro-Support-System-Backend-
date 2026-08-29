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
        /// Get all sessions — filterable by org, therapist, child, status, type, date range.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="therapistId"></param>
        /// <param name="childProfileId"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? organizationId, [FromQuery] Guid? therapistId,
                                                [FromQuery] Guid? childProfileId, [FromQuery] SessionStatus? status,
                                                [FromQuery] SessionType? type, [FromQuery] DateTime? fromDate,
                                                [FromQuery] DateTime? toDate)
        {
            return Ok(await Mediator.Send(new GetSessionsQuery
            {
                OrganizationId = organizationId,
                TherapistId = therapistId,
                ChildProfileId = childProfileId,
                Status = status,
                Type = type,
                FromDate = fromDate,
                ToDate = toDate,
            }));
        }

        /// <summary>
        /// Get a specific session with full details including child records and goal logs.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetSessionQuery { Id = id }));
        }

        /// <summary>
        /// Create a new session — individual or group, single or recurring.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update session details — date, time, duration, notes.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateSessionCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Start a scheduled session — changes status to InProgress.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/start")]
        public async Task<IActionResult> Start(Guid id)
        {
            return Ok(await Mediator.Send(new StartSessionCommand { Id = id }));
        }

        /// <summary>
        /// Mark a session as complete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(Guid id, CompleteSessionCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Cancel a session — single occurrence or entire recurring series.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id, CancelSessionCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Mark a session as no-show — child, therapist, or both.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/no-show")]
        public async Task<IActionResult> MarkNoShow(Guid id, MarkNoShowCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Add or update session notes and goal progress for a specific child. 
        /// Can be called during or after the session.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/records")]
        public async Task<IActionResult> AddOrUpdateChildRecord(Guid id, AddOrUpdateChildSessionRecordCommand command)
        {
            command.SessionId = id;
            return Ok(await Mediator.Send(command));
        }

    }
}