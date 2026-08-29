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
    public class CompleteSessionCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string? GeneralNotes { get; set; }

        public class CompleteSessionCommandHandler(ISessionRepositoryAsync sessionRepository,
                                           IAuthenticatedUserService authenticatedUser) 
              : IRequestHandler<CompleteSessionCommand, Response<bool>>
        {
            private readonly ISessionRepositoryAsync _sessionRepository = sessionRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;

            public async Task<Response<bool>> Handle(CompleteSessionCommand command, CancellationToken cancellationToken)
            {
                using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var session = await _sessionRepository.GetById(command.Id) ??
                              throw new ApiException("Session could not be found.");

                if (session.Status != SessionStatus.InProgress)
                    throw new ApiException("Only in-progress sessions can be completed.");

                session.Status = SessionStatus.Completed;
                session.GeneralNotes = command.GeneralNotes;
                session.LastModified = DateTime.UtcNow;
                session.LastModifiedBy = _authenticatedUser.UserId;

                await _sessionRepository.UpdateAsync(session);

                ts.Complete();

                return new Response<bool>(true, "Session completed successfully.");
            }
        }
    }
}
