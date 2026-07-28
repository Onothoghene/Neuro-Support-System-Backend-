using Application.DTOs.ChildTherapistAssignment;
using Application.DTOs.CommonNodes;
using Application.DTOs.ParentProfile;
using Application.DTOs.TherapyGoal;
using System;
using System.Collections.Generic;

namespace Application.DTOs.ChildProfile
{
    public class ChildProfileVM : AuditableBaseEntityVM
    {
        public string FirstName { get; set; }
        public string? OtherName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age => CalculateAge(DateOfBirth);
        public string? Gender { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string DiagnosisName { get; set; }
        public string? DiagnosisCode { get; set; }
        public DateTime? DiagnosisDate { get; set; }
        public string? DiagnosedBy { get; set; }
        public string? MedicalHistory { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? EmergencyContactRelationship { get; set; }
        public bool IsActive { get; set; }
        public List<TherapyGoalVM> TherapyGoals { get; set; } = [];
        public List<ChildTherapistAssignmentVM> AssignedTherapists { get; set; } = [];
        public List<ParentProfileVM> Parents { get; set; } = [];

        private static int CalculateAge(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;
            return age;
        }
    }

    public class AddChildProfileRequest
    {
        public required string FirstName { get; set; }
        public string? OtherName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public Guid DiagnosisTypeId { get; set; }
        public DateTime? DiagnosisDate { get; set; }
        public string? DiagnosedBy { get; set; }
        public string? MedicalHistory { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? EmergencyContactRelationship { get; set; }

        /// <summary>Required for org therapists — null for freelancers.</summary>
        public Guid? OrganizationId { get; set; }

        // Initial goals (optional on creation)
        public List<AddTherapyGoalRequest> TherapyGoals { get; set; } = new();

        // Initial parents (optional on creation)
        public List<AddParentProfileRequest> Parents { get; set; } = new();
    }

    public class UpdateChildProfileRequest
    {
        public string? FirstName { get; set; }
        public string? OtherName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public Guid? DiagnosisTypeId { get; set; }
        public DateTime? DiagnosisDate { get; set; }
        public string? DiagnosedBy { get; set; }
        public string? MedicalHistory { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? EmergencyContactRelationship { get; set; }
    }

    public class ChildProfileSummaryVM
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string DiagnosisName { get; set; }
        public bool IsActive { get; set; }
        public int TotalGoals { get; set; }
        public int AchievedGoals { get; set; }
        public int InProgressGoals { get; set; }
    }

}
