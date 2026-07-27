using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class TherapyGoal : AuditableBaseEntity
    {
        public Guid ChildProfileId { get; set; }
        public Guid GoalCategoryId { get; set; }

        // ── Structured
        public string? Title { get; set; }
        public GoalStatus Status { get; set; } = GoalStatus.NotStarted;
        public DateTime? TargetDate { get; set; }

        // ── Free text
        public string? Description { get; set; }
        public string? Notes { get; set; }

        // Navigation
        public ChildProfile ChildProfile { get; set; }
        public GoalCategory GoalCategory { get; set; }
    }

    public enum GoalStatus
    {
        NotStarted = 0,
        InProgress = 1,
        Achieved = 2,
        OnHold = 3
    }
}
