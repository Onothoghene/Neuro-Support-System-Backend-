using Domain.Enums;
using System;

namespace Application.DTOs.GoalProgressLog
{
    public class GoalProgressLogRequest
    {
        public Guid TherapyGoalId { get; set; }
        public string? ProgressNote { get; set; }

        /// <summary>1-5 rating.</summary>
        public int? ProgressRating { get; set; }

        /// <summary>Optional — update goal status after this session.</summary>
        public GoalStatus? StatusUpdate { get; set; }
    }

    public class GoalProgressLogVM
    {
        public Guid Id { get; set; }
        public Guid TherapyGoalId { get; set; }
        public string GoalTitle { get; set; }
        public string GoalCategory { get; set; }
        public string? ProgressNote { get; set; }
        public int? ProgressRating { get; set; }
        public string? StatusUpdate { get; set; }
    }
}
