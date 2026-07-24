using Application.DTOs.Users;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Query
{
    public class GetUserProfileQuery : IRequest<Response<UserDetailsVM>>
    {
        public Guid? Id { get; set; }

        public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, Response<UserDetailsVM>>
        {
            private readonly IUserProfileRepositoryAsync _userProfile;
            private readonly IMapper _mapper;
            private readonly IAuthenticatedUserService _user;

            public GetUserProfileQueryHandler(IUserProfileRepositoryAsync userProfile, IMapper mapper,
                                              IAuthenticatedUserService user)
            {
                _userProfile = userProfile;
                _mapper = mapper;
                _user = user;
            }

            public async Task<Response<UserDetailsVM>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
            {
                var userId = request.Id != Guid.Empty ? request.Id.Value : Guid.Parse(_user.UserId);

                var personalDetails = await _userProfile.GetUserByIdAsync(userId) ?? throw new ApiException("User profile details not found");

                var resp = _mapper.Map<UserDetailsVM>(personalDetails);

                return new Response<UserDetailsVM>(resp, "User profile retrieved successfully.");
            }
        }
    }
}
