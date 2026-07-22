using Application.Features.Organizations.Command;
using Application.Features.OrganizationUsers.Command;
using Application.Features.OrganizationUsers.Query;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/organization/users")]
    public class OrganizationUsersController : BaseApiController
    {
        /// <summary>
        /// Get organization members.
        /// Filterable by role, status, name/email, and join date.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{organizationId?}")]
        public async Task<IActionResult> GetOrganizationMembers(Guid organizationId, [FromQuery] string? roleName,
                                                         [FromQuery] bool? isActive, [FromQuery] string? searchTerm,
                                                         [FromQuery] DateTime? joinedFrom, [FromQuery] DateTime? joinedTo)
        {
            return Ok(await Mediator.Send(new GetOrganizationMembersQuery { OrganizationId = organizationId }));
        }

        /// <summary>
        /// update organization details
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("")]
        public async Task<IActionResult> UpdateOrganization(UpdateOrganizationsCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Remove a member/user from an organization. Only organization admins/owners can perform this action.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{organizationId}/members/{targetUserId}")]
        public async Task<IActionResult> RemoveOrganizationMember(Guid OrganizationId, Guid TargetUserId)
        {
            return Ok(await Mediator.Send(new RemoveOrganizationMemberCommand { OrganizationId = OrganizationId, TargetUserId = TargetUserId }));
        }
    }
}
