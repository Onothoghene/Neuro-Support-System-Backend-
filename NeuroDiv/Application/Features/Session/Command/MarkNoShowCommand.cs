using Application.Exceptions;
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
    public class MarkNoShowCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public NoShowType NoShowType { get; set; }
        public string? Notes { get; set; }

        public class MarkNoShowCommandHandler(ISessionRepositoryAsync sessionRepository,
                                           IAuthenticatedUserService authenticatedUser) 
              : IRequestHandler<MarkNoShowCommand, Response<bool>>
        {
            private readonly ISessionRepositoryAsync _sessionRepository = sessionRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;

            public async Task<Response<bool>> Handle(MarkNoShowCommand command, CancellationToken cancellationToken)
            {
                using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var session = await _sessionRepository.GetById(command.Id) ??
                              throw new ApiException("Session could not be found.");

                if (session.Status == SessionStatus.Completed)
                    throw new ApiException("Completed sessions cannot be marked as no-show.");

                if (session.Status == SessionStatus.Cancelled)
                    throw new ApiException("Cancelled sessions cannot be marked as no-show.");

                session.Status = SessionStatus.NoShow;
                session.NoShowType = command.NoShowType;
                session.NoShowNotes = command.Notes;
                session.LastModified = DateTime.UtcNow;
                session.LastModifiedBy = _authenticatedUser.UserId;

                await _sessionRepository.UpdateAsync(session);

                ts.Complete();

                return new Response<bool>(true, "Session marked as no-show.");
            }
        }
    }
}
