using Application.Features.Organizations.Command;
using Application.Features.Organizations.Query;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrganizationsController : BaseApiController
    {
        /// <summary>
        /// Get Organization details
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("{organizationId?}")]
        public async Task<IActionResult> GetOrganization(Guid organizationId)
        {
            return Ok(await Mediator.Send(new GetOrganizationsByIdQuery { OrganizationId = organizationId }));
        }

        /// <summary>
        /// update organization details
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("")]
        public async Task<IActionResult> UpdateOrganization(UpdateOrganizationsCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
