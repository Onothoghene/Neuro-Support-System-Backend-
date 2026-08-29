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
    public class StartSessionCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class StartSessionCommandHandler(ISessionRepositoryAsync sessionRepository,
                                           IAuthenticatedUserService authenticatedUser) 
              : IRequestHandler<StartSessionCommand, Response<bool>>
        {
            private readonly ISessionRepositoryAsync _sessionRepository = sessionRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;

            public async Task<Response<bool>> Handle(StartSessionCommand command, CancellationToken cancellationToken)
            {
                using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var session = await _sessionRepository.GetById(command.Id) ??
                              throw new ApiException("Session could not be found.");

                if (session.Status != SessionStatus.Scheduled)
                    throw new ApiException("Only scheduled sessions can be started.");

                session.Status = SessionStatus.InProgress;
                session.LastModified = DateTime.UtcNow;
                session.LastModifiedBy = _authenticatedUser.UserId;

                await _sessionRepository.UpdateAsync(session);

                ts.Complete();

                return new Response<bool>(true, "Session started.");
            }
        }
    }
}
