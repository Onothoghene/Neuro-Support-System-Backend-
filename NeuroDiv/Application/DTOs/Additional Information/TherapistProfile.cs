using Application.DTOs.CommonNodes;
using System;
using System.Collections.Generic;

namespace Application.DTOs.File
{
    public class TherapistSpecializationRequest
    {
        public string? Condition { get; set; }
        public string? Notes { get; set; }
    }

    public class TherapistProfileVM : AuditableBaseEntityVM
    {
        public Guid UserId { get; set; }
        public string? Bio { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? LicenseNumber { get; set; }
        public string? LicenseType { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public bool IsAvailableForNewClients { get; set; }
        public bool IsPublicProfile { get; set; }
        public List<TherapistSpecializationVM> Specializations { get; set; } = new();

    }

    public class TherapistSpecializationVM : BaseEntityVM
    {
        public string? Condition { get; set; }
        public string? Notes { get; set; }
    }
}
