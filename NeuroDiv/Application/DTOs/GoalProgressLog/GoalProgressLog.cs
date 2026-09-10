using Domain.Enums;
using System;

namespace Application.DTOs.GoalProgressLog
{
    public class GoalProgressLogRequest
    {
        public Guid TherapyGoalId { get; set; }
        public string? ProgressNote { get; set; }
        public int? ProgressRating { get; set; }
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
