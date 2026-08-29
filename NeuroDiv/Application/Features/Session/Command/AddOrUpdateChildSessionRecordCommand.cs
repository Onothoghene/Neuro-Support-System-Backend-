using Application.DTOs.GoalProgressLog;
using Application.Exceptions;
using Application.Helper;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
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
    public class AddOrUpdateChildSessionRecordCommand : IRequest<Response<bool>>
    {
        public Guid SessionId { get; set; }
        public Guid ChildProfileId { get; set; }
        public string? GeneralNotes { get; set; }
        public ChildEngagement? Engagement { get; set; }
        public List<GoalProgressLogRequest> GoalProgressLogs { get; set; } = [];

        public class AddOrUpdateChildSessionRecordCommandHandler(ISessionRepositoryAsync sessionRepository,
                                                                 IAuthenticatedUserService authenticatedUser,
                                                                 IChildSessionRecordRepositoryAsync recordRepository,
                                                                 ITherapyGoalRepositoryAsync goalRepository) 
              : IRequestHandler<AddOrUpdateChildSessionRecordCommand, Response<bool>>
        {
            private readonly ISessionRepositoryAsync _sessionRepository = sessionRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;
            private readonly IChildSessionRecordRepositoryAsync _recordRepository = recordRepository;
            private readonly ITherapyGoalRepositoryAsync _goalRepository = goalRepository;

            public async Task<Response<bool>> Handle(AddOrUpdateChildSessionRecordCommand command, CancellationToken cancellationToken)
            {
                using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var session = await _sessionRepository.GetById(command.SessionId) ??
                              throw new ApiException("Session could not be found.");

                if (session.Status == SessionStatus.Cancelled)
                    throw new ApiException("Cannot add notes to a cancelled session.");

                var existingRecord = await _recordRepository.GetBySessionAndChildAsync(command.SessionId, command.ChildProfileId);

                if (existingRecord != null)
                {
                    //Update existing record
                    existingRecord.GeneralNotes = command.GeneralNotes;
                    existingRecord.Engagement = command.Engagement;

                    // Replace goal progress logs
                    existingRecord.GoalProgressLogs.Clear();
                    existingRecord.GoalProgressLogs = SessionBuilder.BuildGoalProgressLogs(command.GoalProgressLogs);

                    await _recordRepository.UpdateAsync(existingRecord);
                }
                else
                {
                    //Create new record 
                    var record = new Domain.Entities.ChildSessionRecord
                    {
                        SessionId = command.SessionId,
                        ChildProfileId = command.ChildProfileId,
                        GeneralNotes = command.GeneralNotes,
                        Engagement = command.Engagement,
                        GoalProgressLogs = SessionBuilder.BuildGoalProgressLogs(command.GoalProgressLogs),
                    };

                    await _recordRepository.AddAsync(record);
                }

                // Update goal statuses if therapist changed them
                foreach (var log in command.GoalProgressLogs.Where(l => l.StatusUpdate.HasValue))
                {
                    var goal = await _goalRepository.GetByIdAsync(log.TherapyGoalId);
                    if (goal != null)
                    {
                        goal.Status = log.StatusUpdate!.Value;

                        await _goalRepository.UpdateAsync(goal);
                    }
                }

                ts.Complete();

                return new Response<bool>(true, "Session record saved successfully.");
            }
        }
    }
}
