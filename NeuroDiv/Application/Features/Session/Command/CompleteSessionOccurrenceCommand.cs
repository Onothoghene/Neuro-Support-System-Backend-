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
    public class CompleteSessionOccurrenceCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string? GeneralNotes { get; set; }
        public DateTime? ActualEndTime { get; set; }

        public class CompleteSessionOccurrenceCommandHandler(ISessionOccurrenceRepositoryAsync occurrenceRepository,
                                                             IAuthenticatedUserService authenticatedUser) 
              : IRequestHandler<CompleteSessionOccurrenceCommand, Response<bool>>
        {
            private readonly ISessionOccurrenceRepositoryAsync _occurrenceRepository = occurrenceRepository;
            private readonly IAuthenticatedUserService _authenticatedUser = authenticatedUser;

            public async Task<Response<bool>> Handle(CompleteSessionOccurrenceCommand command, CancellationToken cancellationToken)
            {
                using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var occurrence = await _occurrenceRepository.GetByIdAsync(command.Id) ??
                                 throw new ApiException("Session occurrence could not be found.");

                //if (occurrence.Status != SessionStatus.InProgress)
                //    throw new ApiException("Only in-progress session can be completed.");

                occurrence.Status = SessionStatus.Completed;
                occurrence.GeneralNotes = command.GeneralNotes;
                occurrence.ActualEndTime = command.ActualEndTime;

                await _occurrenceRepository.UpdateAsync(occurrence);

                ts.Complete();

                return new Response<bool>(true, "Session occurrence completed successfully.");
            }
        }
    }
}