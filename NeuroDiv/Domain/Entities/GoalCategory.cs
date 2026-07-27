using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class GoalCategory : AuditableBaseEntity
    {
        public GoalCategory()
        {
            TherapyGoals = new HashSet<TherapyGoal>();
        }

        public required string Name { get; set; }   // "Communication", "Motor Skills" etc.
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<TherapyGoal> TherapyGoals { get; set; }
    }
}
