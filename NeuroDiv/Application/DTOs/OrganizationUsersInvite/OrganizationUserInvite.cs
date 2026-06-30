using Application.DTOs.CommonNodes;
using System;

namespace Application.DTOs.OrganizationUsersInvite
{
    public class OrganizationUserInviteVM : AuditableBaseEntityVM
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAccepted { get; set; } = false;
    }

}
