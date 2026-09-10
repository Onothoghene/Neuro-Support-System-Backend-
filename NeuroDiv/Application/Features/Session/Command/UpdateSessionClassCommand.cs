using Application.DTOs.Session;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Features.Session.Command
{
    public class UpdateSessionClassCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public SessionMode? Mode { get; set; }
        public bool? IsActive { get; set; }
        public OnlineSessionDetailsRequest? OnlineDetails { get; set; }

        public class UpdateSessionClassCommandHandler(ISessionClassRepositoryAsync sessionClassRepository, IMapper mapper)
              : IRequestHandler<UpdateSessionClassCommand, Response<bool>>
        {
            private readonly ISessionClassRepositoryAsync _sessionClassRepository = sessionClassRepository;
            private readonly IMapper _mapper = mapper;

            public async Task<Response<bool>> Handle(UpdateSessionClassCommand command, CancellationToken cancellationToken)
            {
                using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var sessionClass = await _sessionClassRepository.GetByIdWithDetailsAsync(command.Id) ??
                                   throw new ApiException("Session could not be found.");

                if (!string.IsNullOrWhiteSpace(command.Title))
                    sessionClass.Title = command.Title;

                sessionClass.Description = command.Description;

                if (command.Mode.HasValue)
                    sessionClass.Mode = command.Mode.Value;

                if (command.IsActive.HasValue)
                    sessionClass.IsActive = command.IsActive.Value;

                // Handle online details
                if (command.OnlineDetails != null)
                {
                    if (sessionClass.OnlineDetails != null)
                    {
                        sessionClass.OnlineDetails.Platform = command.OnlineDetails.Platform;
                        sessionClass.OnlineDetails.MeetingLink = command.OnlineDetails.MeetingLink;
                        sessionClass.OnlineDetails.MeetingId = command.OnlineDetails.MeetingId;
                        sessionClass.OnlineDetails.MeetingPassword = command.OnlineDetails.MeetingPassword;
                        sessionClass.OnlineDetails.JoiningInstructions = command.OnlineDetails.JoiningInstructions;
                    }
                    else
                    {
                        var sessionMode = _mapper.Map<SessionOnlineDetails>(command.OnlineDetails);

                        sessionClass.OnlineDetails = sessionMode;
                    }
                }
                await _sessionClassRepository.UpdateAsync(sessionClass);

                ts.Complete();

                return new Response<bool>(true, "Session updated successfully.");
            }
        }
    }
}
