using Application.DTOs.CommonNodes;

namespace Application.DTOs.Organizations
{
    public class OrganizationsVM : AuditableBaseEntityVM
    {
        public required string Name { get; set; }
        public string Type { get; set; }
        public string? LogoUrl { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

    }

}
