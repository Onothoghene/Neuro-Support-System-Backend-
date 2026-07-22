using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Defaults;
using Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public class OrganizationPermissionService : IOrganizationPermissionService
    {
        private readonly IOrganizationUserRolesRepositoryAsync _orgUserRolesRepository;

        public OrganizationPermissionService(IOrganizationUserRolesRepositoryAsync orgUserRolesRepository)
        {
            _orgUserRolesRepository = orgUserRolesRepository;
        }

        public async Task<OrganizationRoles?> GetUserOrgRoleAsync(string userId, Guid organizationId)
        {
            var userRole = await _orgUserRolesRepository.GetUserRoleInOrgAsync(Guid.Parse(userId), organizationId);

            return userRole?.OrganizationRoles;
        }

        public async Task<bool> HasRoleAsync(string userId, Guid organizationId, params string[] roles)
        {
            var userRole = await GetUserOrgRoleAsync(userId, organizationId);
            if (userRole == null) return false;

            return roles.Contains(userRole.Name);
        }

        public async Task EnsureHasRoleAsync(string userId, Guid organizationId, params string[] roles)
        {
            var hasRole = await HasRoleAsync(userId, organizationId, roles);
            if (!hasRole)
                throw new ApiException("You do not have permission to perform this action.");
        }

        public async Task EnsureCanRemoveMemberAsync(string requestingUserId, Guid organizationId, Guid targetUserId)
        {
            var requestingRole = await GetUserOrgRoleAsync(requestingUserId, organizationId) ??
                                 throw new ApiException("You are not a member of this organization.");

            var targetRole = await GetUserOrgRoleAsync(targetUserId.ToString(), organizationId) ?? 
                             throw new ApiException("The target user is not a member of this organization.");

            // Nobody can remove the Clinic Owner — must transfer first
            if (targetRole.Name == DefaultOrganizationRoles.ClinicOwner)
                throw new ApiException("The Clinic Owner cannot be removed. Please transfer ownership first.");

            // Clinic Owner can remove anyone except themselves
            if (requestingRole.Name == DefaultOrganizationRoles.ClinicOwner)
                return;

            // Clinic Admin can only remove Lead Therapist and below
            if (requestingRole.Name == DefaultOrganizationRoles.ClinicAdmin)
            {
                if (targetRole.Name == DefaultOrganizationRoles.ClinicAdmin)
                    throw new ApiException("Clinic Admins cannot remove other Clinic Admins.");
                return;
            }

            // Lead Therapist and Therapist cannot remove anyone
            throw new ApiException("You do not have permission to remove members.");
        }

        public async Task EnsureCanUpdateOrgAsync(string requestingUserId, Guid organizationId, bool isDomainChange = false)
        {
            var requestingRole = await GetUserOrgRoleAsync(requestingUserId, organizationId) ?? 
                                 throw new ApiException("You are not a member of this organization.");

            // Domain changes restricted to Clinic Owner only
            if (isDomainChange && requestingRole.Name != DefaultOrganizationRoles.ClinicOwner)
                throw new ApiException("Only the Clinic Owner can change the organization domain.");

            // Clinic Owner and Clinic Admin can update everything else
            if (requestingRole.Name == DefaultOrganizationRoles.ClinicOwner ||
                requestingRole.Name == DefaultOrganizationRoles.ClinicAdmin)
                return;

            throw new ApiException("You do not have permission to update organization details.");
        }

        public async Task EnsureCanManageRolesAsync(string requestingUserId, Guid organizationId)
        {
            await EnsureHasRoleAsync(requestingUserId, organizationId, DefaultOrganizationRoles.ClinicOwner, DefaultOrganizationRoles.ClinicAdmin);
        }

        public bool HasPermission(string userId, Guid organizationId, string permission)
        {
            // This is a placeholder implementation. In a real application, you would check the user's role and associated permissions.
            // For example, you might have a dictionary or database table that maps roles to permissions.
            // Here, we'll assume that Clinic Owner and Clinic Admin have all permissions, while Lead Therapist and Therapist have limited permissions.
            var userRoleTask = GetUserOrgRoleAsync(userId, organizationId);
            userRoleTask.Wait(); // Wait for the async method to complete
            var userRole = userRoleTask.Result;
            if (userRole == null) return false;
            return userRole.Name switch
            {
                DefaultOrganizationRoles.ClinicOwner => true,
                DefaultOrganizationRoles.ClinicAdmin => true,
                DefaultOrganizationRoles.LeadTherapist => permission != "ManageRoles" && permission != "ChangeDomain",
                DefaultOrganizationRoles.Therapist => permission == "ViewOrganization",
                _ => false,
            };
        }

    }
}
