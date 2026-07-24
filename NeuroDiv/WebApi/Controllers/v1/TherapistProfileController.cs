using Application.Features.TherapistProfile.Command;
using Application.Features.TherapistProfiles.Queries;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class TherapistProfileController : BaseApiController
    {
        /// <summary>
        /// Get a specific therapist's profile by their UserProfileId.
        /// Used by org admins viewing their therapists.
        /// Can be used to retrieve both the logged-in therapist's own profile and other therapists' profiles.
        /// </summary>
        [HttpGet("{userProfileId?}")]
        public async Task<IActionResult> GetProfile(Guid userProfileId)
        {
            return Ok(await Mediator.Send(new GetTherapistProfileQuery { UserProfileId = userProfileId }));
        }

        /// <summary>
        /// Create or update own therapist profile.
        /// Automatically creates if doesn't exist, updates if it does.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(AddOrUpdateTherapistProfileCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
