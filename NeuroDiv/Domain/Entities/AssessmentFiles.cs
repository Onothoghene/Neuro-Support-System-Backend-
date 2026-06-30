using Domain.Common;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    public class AssessmentFiles : AuditableBaseEntity
    {
        public int ChildId { get; set; }
        public int? UploadedByUserId { get; set; }
        public required string OriginalFileName { get; set; }
        public required string StoredPath { get; set; }
        public string ContentType { get; set; } = "application/pdf";
        public long FileSizeBytes { get; set; }
        public string AssessmentType { get; set; }
        public string AssessmentDate { get; set; }
        public string Notes { get; set; }
        public string VerificationStatus { get; set; } = "pending";
        [AllowNull]
        public string ReviewNotes { get; set; }

        //public Child Child { get; set; } = null!;
        public UserProfile? UploadedByUser { get; set; }
    }
}
