using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Features.Session.Command
{
    public class CancelSessionOccurrenceCommand : IRequest<Response<bool>>
    {
        public Guid OccurrenceId { get; set; }
        public CancellationReason Reason { get; set; }
        public string? Notes { get; set; }

        public class CancelSessionOccurrenceCommandHandler(ISessionOccurrenceRepositoryAsync occurrenceRepository,
                                                           IAuthenticatedUserService authenticatedUser,
                                                           ISessionCancellationRepositoryAsync cancellationRepository)
               : IRequestHandler<CancelSessionOccurrenceCommand, Response<bool>>
        {
            private readonly ISessionOccurrenceRepositoryAsync _occurrenceRepository = occurrenceRepository;
            private readonly ISessionCancellationRepositoryAsync _cancellationRepository = cancellationRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;
            
            public async Task<Response<bool>> Handle(CancelSessionOccurrenceCommand command, CancellationToken cancellationToken)
            {
                using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var occurrence = await _occurrenceRepository.GetByIdWithDetailsAsync(command.OccurrenceId) ?? 
                                 throw new ApiException("Session occurrence could not be found.");

                if (occurrence.Status == SessionStatus.Completed) 
                    throw new ApiException("Completed sessions cannot be cancelled.");

                if (occurrence.Status == SessionStatus.Cancelled) 
                    throw new ApiException("This session is already cancelled.");

                occurrence.Status = SessionStatus.Cancelled;

                await _occurrenceRepository.UpdateAsync(occurrence);

                await _cancellationRepository.AddAsync(new SessionCancellation
                {
                    SessionOccurrenceId = occurrence.Id,
                    Reason = command.Reason,
                    Notes = command.Notes,
                    CancelledAt = DateTime.UtcNow,
                    CancelledBy = _authenticatedUser.UserId,
                });

                ts.Complete();

                // Series continues — Hangfire generates next occurrence automatically
                return new Response<bool>(true, "Session cancelled. Future sessions in this series are unaffected.");
            }
        }
    }
}
