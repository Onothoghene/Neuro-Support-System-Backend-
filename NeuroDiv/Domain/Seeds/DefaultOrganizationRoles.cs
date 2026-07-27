using System.Collections.Generic;

namespace Domain.Seeds
{
    public static class DefaultOrganizationRoles
    {
        public const string ClinicOwner = "Clinic Owner";
        public const string ClinicAdmin = "Clinic Admin";
        public const string LeadTherapist = "Lead Therapist";
        public const string Therapist = "Therapist";

        public static List<(string Name, string Description)> GetDefaults() => new()
        {
            (
                ClinicOwner,
                "God-mode access within the organization. Handles billing, legal agreements, and organization-wide settings."
            ),
            (
                ClinicAdmin,
                "Manages day-to-day operations, staff accounts, schedule settings, and administrative workflows."
            ),
            (
                LeadTherapist,
                "Manages clinical quality, reviews peer assessments, approves clinical reports, and ensures standard adherence."
            ),
            (
                Therapist,
                "Direct patient care, manages their specific caseload, writes daily reports, and performs assessments."
            ),
        };
    }
}