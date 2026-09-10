using Application.DTOs.GoalProgressLog;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
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
    public class AddOrUpdateChildSessionRecordCommand : IRequest<Response<bool>>
    {
        public Guid SessionOccurrenceId { get; set; }
        public Guid ChildProfileId { get; set; }
        public string? GeneralNotes { get; set; }
        public ChildEngagement? Engagement { get; set; }
        public List<GoalProgressLogRequest> GoalProgressLogs { get; set; } = new();

        public class AddOrUpdateChildSessionRecordCommandHandler(ISessionOccurrenceRepositoryAsync occurrenceRepository,
                                                                 IAuthenticatedUserService authenticatedUser,
                                                                 IChildSessionRecordRepositoryAsync recordRepository,
                                                                 ITherapyGoalRepositoryAsync goalRepository,
                                                                 IMapper mapper)
              : IRequestHandler<AddOrUpdateChildSessionRecordCommand, Response<bool>>
        {
            private readonly ISessionOccurrenceRepositoryAsync _occurrenceRepository = occurrenceRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;
            private readonly IChildSessionRecordRepositoryAsync _recordRepository = recordRepository;
            private readonly IMapper _mapper = mapper;
            private readonly ITherapyGoalRepositoryAsync _goalRepository = goalRepository;

            public async Task<Response<bool>> Handle(AddOrUpdateChildSessionRecordCommand command, CancellationToken cancellationToken)
            {
                using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var occurrence = await _occurrenceRepository.GetByIdWithDetailsAsync(command.SessionOccurrenceId)
                                 ?? throw new ApiException("Session occurrence could not be found.");

                if (occurrence.Status == SessionStatus.Cancelled)
                    throw new ApiException("Cannot add notes to a cancelled session.");

                var existingRecord = await _recordRepository.GetByOccurrenceAndChildAsync(command.SessionOccurrenceId, 
                                                                                          command.ChildProfileId);

                var goalLogs = _mapper.Map<List<GoalProgressLog>>(command.GoalProgressLogs);

                if (existingRecord != null)
                {
                    // Update
                    _mapper.Map(command, existingRecord);
                    existingRecord.GoalProgressLogs.Clear();
                    existingRecord.GoalProgressLogs = goalLogs;

                    await _recordRepository.UpdateAsync(existingRecord);
                }
                else
                {
                    // Create
                    var newRecord  = _mapper.Map<ChildSessionRecord>(command);
                    newRecord.SessionOccurrenceId = command.SessionOccurrenceId;
                    newRecord.GoalProgressLogs = goalLogs;

                    await _recordRepository.AddAsync(newRecord);
                }

                // Update goal statuses where therapist changed them
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