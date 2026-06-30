using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Assessment : AuditableBaseEntity
    {
        public int ChildId { get; set; }
        public int? ConductedByUserId { get; set; }
        public required string AssessmentDate { get; set; }
        public required string AssessmentType { get; set; }
        public double Score { get; set; }
        public double? MaxScore { get; set; }
        public string? Domain { get; set; }
        public string Notes { get; set; } = "";
        public string? ConductedByName { get; set; }

        //public Child Child { get; set; } = null!;
        public UserProfile? ConductedByUser { get; set; }
    }
}
