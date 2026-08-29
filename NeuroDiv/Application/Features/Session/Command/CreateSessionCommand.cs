using Application.Exceptions;
using Application.Helper;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Features.Session.Command
{
    public class CreateSessionCommand : IRequest<Response<Guid>>
    {
        public string Title { get; set; }
        public SessionType Type { get; set; } = SessionType.Individual;
        public DateTime ScheduledDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Guid? SessionDurationId { get; set; }
        public Guid? OrganizationId { get; set; }
        public List<Guid> ChildProfileIds { get; set; } = new();
        public bool IsRecurring { get; set; } = false;
        public RecurrencePattern? RecurrencePattern { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }

        public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, Response<Guid>>
        {
            private readonly ISessionRepositoryAsync _sessionRepository;
            private readonly IAuthenticatedUserService _authenticatedUser;

            public CreateSessionCommandHandler(ISessionRepositoryAsync sessionRepository,
                                               IAuthenticatedUserService authenticatedUser)
            {
                _sessionRepository = sessionRepository;
                _authenticatedUser = authenticatedUser;
            }
            public async Task<Response<Guid>> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
            {
                using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var therapistId = Guid.Parse(_authenticatedUser.UserId);

                // Validate individual session only has one child
                if (command.Type == SessionType.Individual && command.ChildProfileIds.Count > 1)
                    throw new ApiException("Individual sessions can only have one child. " +
                                           "Please use Group session type for multiple children.");

                if (command.ChildProfileIds.Count == 0)
                    throw new ApiException("At least one child must be added to the session.");

                var seriesId = command.IsRecurring ? Guid.NewGuid() : (Guid?)null;

                // Build child session records for each child
                var childRecords = command.ChildProfileIds.Select(childId => new ChildSessionRecord
                {
                    ChildProfileId = childId,
                }).ToList();

                if (!command.IsRecurring)
                {
                    // ── Single session
                    var session = SessionBuilder.BuildSession(command, therapistId, seriesId, childRecords);
                    var result = await _sessionRepository.AddAsync(session);

                    ts.Complete();
                    return new Response<Guid>(result.Id, "Session created successfully.");
                }
                else
                {
                    // ── Recurring sessions ────────────────────────────────────
                    if (!command.RecurrencePattern.HasValue)
                        throw new ApiException("Recurrence pattern is required for recurring sessions.");

                    var sessions = SessionBuilder.GenerateRecurringSessions(command, therapistId, 
                                                                           seriesId!.Value, childRecords);

                    await _sessionRepository.AddRangeAsync(sessions);

                    ts.Complete();
                    return new Response<Guid>(
                        seriesId!.Value,
                        $"{sessions.Count} recurring sessions created successfully.");
                }

            }
        }
    }
}
