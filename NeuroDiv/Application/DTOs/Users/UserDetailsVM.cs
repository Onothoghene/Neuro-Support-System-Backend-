using Application.DTOs.CommonNodes;

namespace Application.DTOs.Users
{
    public class UserDetailsVM : AuditableBaseEntityVM
    {
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Gender { get; set; }
    }

}
