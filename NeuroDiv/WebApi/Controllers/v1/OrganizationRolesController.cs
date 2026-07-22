using Application.Features.OrganizationRoles.Command;
using Application.Features.OrganizationRoles.Query;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/organization")]
    public class OrganizationRolesController : BaseApiController
    {
        /// <summary>
        /// Get all roles in the organization.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{organizationId}/roles")]
        public async Task<IActionResult> GetOrganizationRoles(Guid organizationId)
        {
            return Ok(await Mediator.Send(new GetOrganizationRolesByOrgIdQuery { OrganizationId = organizationId }));
        }

        /// <summary>
        /// Add a custom role or update an existing one.
        /// Pass Id to update, omit Id to create.
        /// </summary>
        [HttpPut("roles")]
        public async Task<IActionResult> AddOrUpdateRole(AddOrUpdateOrganizationRolesCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
