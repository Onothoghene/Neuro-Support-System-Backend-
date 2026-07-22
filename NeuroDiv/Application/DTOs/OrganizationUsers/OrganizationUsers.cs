using Application.DTOs.CommonNodes;
using System;

namespace Application.DTOs.OrganizationUsers
{
    public class OrganizationUsersVM : AuditableBaseEntityVM
    {
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime JoinedAt { get; set; }
    }

    public class UserOrganizationVM
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
