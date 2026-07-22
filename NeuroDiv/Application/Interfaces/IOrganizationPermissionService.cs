using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrganizationPermissionService
    {
        /// <summary>Gets the requesting user's role in a specific org.</summary>
        Task<OrganizationRoles?> GetUserOrgRoleAsync(string userId, Guid organizationId);

        /// <summary>Checks if the user has one of the specified roles in the org.</summary>
        Task<bool> HasRoleAsync(string userId, Guid organizationId, params string[] roles);

        /// <summary>Throws if the user doesn't have one of the specified roles.</summary>
        Task EnsureHasRoleAsync(string userId, Guid organizationId, params string[] roles);

        /// <summary>Checks if the requesting user can remove the target user.</summary>
        Task EnsureCanRemoveMemberAsync(string requestingUserId, Guid organizationId, Guid targetUserId);

        /// <summary>Checks if the requesting user can update org details.</summary>
        Task EnsureCanUpdateOrgAsync(string requestingUserId, Guid organizationId, bool isDomainChange = false);

        /// <summary>Checks if the requesting user can manage roles.</summary>
        Task EnsureCanManageRolesAsync(string requestingUserId, Guid organizationId);
    }
}
