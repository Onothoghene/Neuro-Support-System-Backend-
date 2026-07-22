using Application.DTOs.CommonNodes;
using System;

namespace Application.DTOs.Organizations
{
    public class OrganizationsVM : AuditableBaseEntityVM
    {
        
        public string? LogoUrl { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        //public string? Type { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Domain { get; set; }

    }

}
