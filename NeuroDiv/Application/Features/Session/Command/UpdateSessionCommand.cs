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
    public class UpdateSessionCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public Guid? SessionDurationId { get; set; }
        public string? GeneralNotes { get; set; }

        public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, Response<bool>>
        {
            private readonly ISessionRepositoryAsync _sessionRepository;
            private readonly IAuthenticatedUserService _authenticatedUser;

            public UpdateSessionCommandHandler(ISessionRepositoryAsync sessionRepository,
                                               IAuthenticatedUserService authenticatedUser)
            {
                _sessionRepository = sessionRepository;
                _authenticatedUser = authenticatedUser;
            }
            public async Task<Response<bool>> Handle(UpdateSessionCommand command, CancellationToken cancellationToken)
            {
                using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var session = await _sessionRepository.GetById(command.Id)
                    ?? throw new ApiException("Session could not be found.");

                if (session.Status == SessionStatus.Completed)
                    throw new ApiException("Completed sessions cannot be modified.");

                if (session.Status == SessionStatus.Cancelled)
                    throw new ApiException("Cancelled sessions cannot be modified.");

                if (!string.IsNullOrWhiteSpace(command.Title))
                    session.Title = command.Title;

                if (command.ScheduledDate.HasValue)
                    session.ScheduledDate = command.ScheduledDate.Value;

                if (command.StartTime.HasValue)
                    session.StartTime = command.StartTime.Value;

                if (command.EndTime.HasValue)
                    session.EndTime = command.EndTime.Value;

                if (command.SessionDurationId.HasValue)
                    session.SessionDurationId = command.SessionDurationId;

                session.GeneralNotes = command.GeneralNotes;
                session.LastModified = DateTime.UtcNow;
                session.LastModifiedBy = _authenticatedUser.UserId;

                await _sessionRepository.UpdateAsync(session);

                ts.Complete();

                return new Response<bool>(true, "Session updated successfully.");
            }
        }
    }
}
