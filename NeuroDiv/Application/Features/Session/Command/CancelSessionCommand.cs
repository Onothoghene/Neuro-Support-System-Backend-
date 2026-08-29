using Application.Exceptions;
using Application.Helper;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Features.Session.Command
{
    public class CancelSessionCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public CancelType CancelType { get; set; }
        public CancellationReason Reason { get; set; }
        public string? Notes { get; set; }

        public class CancelSessionCommandHandler : IRequestHandler<CancelSessionCommand, Response<bool>>
        {
            private readonly ISessionRepositoryAsync _sessionRepository;
            private readonly IAuthenticatedUserService _authenticatedUser;

            public CancelSessionCommandHandler(ISessionRepositoryAsync sessionRepository,
                                               IAuthenticatedUserService authenticatedUser)
            {
                _sessionRepository = sessionRepository;
                _authenticatedUser = authenticatedUser;
            }
            public async Task<Response<bool>> Handle(CancelSessionCommand command, CancellationToken cancellationToken)
            {
                using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var loggedInUserId  = Guid.Parse(_authenticatedUser.UserId);

                var session = await _sessionRepository.GetById(command.Id) ?? 
                              throw new ApiException("Session could not be found.");

                if (session.Status == SessionStatus.Completed)
                    throw new ApiException("Completed sessions cannot be cancelled.");

                if (session.Status == SessionStatus.Cancelled)
                    throw new ApiException("This session is already cancelled.");

                if (command.CancelType == CancelType.SingleOccurrence || !session.IsRecurring)
                {
                    // ── Cancel single occurrence ──────────────────────────────
                    SessionBuilder.ApplyCancellation(session, command, loggedInUserId);
                    await _sessionRepository.UpdateAsync(session);
                }
                else
                {
                    // ── Cancel entire series ──────────────────────────────────
                    if (!session.RecurringSeriesId.HasValue)
                        throw new ApiException("This session does not belong to a recurring series.");

                    var futureSessions = await _sessionRepository
                        .GetBySeriesIdAsync(session.RecurringSeriesId.Value);

                    foreach (var s in futureSessions)
                        SessionBuilder.ApplyCancellation(s, command, loggedInUserId);

                    await _sessionRepository.UpdateRangeAsync(futureSessions);
                }

                ts.Complete();

                return new Response<bool>(true,
                    command.CancelType == CancelType.EntireSeries
                        ? "All future sessions in this series have been cancelled."
                        : "Session cancelled successfully.");
            }
        }
    }
}
