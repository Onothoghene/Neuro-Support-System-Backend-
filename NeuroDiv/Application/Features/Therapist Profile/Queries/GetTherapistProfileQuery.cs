using Application.DTOs.File;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.TherapistProfiles.Queries
{
    /// <summary>
    /// Gets a therapist profile.
    /// If UserProfileId is not provided, returns the authenticated user's own profile.
    /// If UserProfileId is provided, returns that specific therapist's profile
    /// (used by org admins viewing their therapists).
    /// </summary>
    public class GetTherapistProfileQuery : IRequest<Response<TherapistProfileVM>>
    {
        /// <summary>
        /// Optional — leave null to get own profile.
        /// Provide a value to get another therapist's profile.
        /// </summary>
        public Guid? UserProfileId { get; set; }

        public class GetTherapistProfileQueryHandler : IRequestHandler<GetTherapistProfileQuery, Response<TherapistProfileVM>>
        {
            private readonly ITherapistProfileRepositoryAsync _therapistProfileRepository;
            private readonly IAuthenticatedUserService _authenticatedUser;
            private readonly IMapper _mapper;

            public GetTherapistProfileQueryHandler(
                ITherapistProfileRepositoryAsync therapistProfileRepository,
                IAuthenticatedUserService authenticatedUser,
                IMapper mapper)
            {
                _therapistProfileRepository = therapistProfileRepository;
                _authenticatedUser = authenticatedUser;
                _mapper = mapper;
            }

            public async Task<Response<TherapistProfileVM>> Handle(GetTherapistProfileQuery query, CancellationToken cancellationToken)
            {
                // If no UserProfileId provided, return own profile
                var targetId = query.UserProfileId ?? Guid.Parse(_authenticatedUser.UserId);

                var profile = await _therapistProfileRepository.GetByUserIdAsync(targetId) ??
                              throw new ApiException("Therapist profile could not be found.");

                var result = _mapper.Map<TherapistProfileVM>(profile);

                return new Response<TherapistProfileVM>(result, "Therapist profile retrieved successfully.");
            }
        }
    }
}
