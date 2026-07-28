using Application.DTOs.CommonNodes;

namespace Application.DTOs.ParentProfile
{
    public class ParentProfileVM : AuditableBaseEntityVM
    {
        public string FirstName { get; set; }
        public string? OtherName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Relationship { get; set; }
        public bool IsPrimaryContact { get; set; }
    }

    public class AddParentProfileRequest
    {
        public required string FirstName { get; set; }
        public string? OtherName { get; set; }
        public required string LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Relationship { get; set; }
        public bool IsPrimaryContact { get; set; } = false;
    }

    public class UpdateParentProfileRequest
    {
        public string? FirstName { get; set; }
        public string? OtherName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Relationship { get; set; }
        public bool? IsPrimaryContact { get; set; }
    }

}
