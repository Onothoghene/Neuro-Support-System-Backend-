using Application.DTOs.CommonNodes;
using Domain.Enums;
using System;

namespace Application.DTOs.TherapyGoal
{
    public class TherapyGoalVM : BaseEntityVM
    {
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string Status { get; set; }
        public string? Description { get; set; }
        public DateTime? TargetDate { get; set; }
        public string? Notes { get; set; }
        public bool IsOverdue => TargetDate.HasValue
                                 && TargetDate.Value < DateTime.Today
                                 && Status != nameof(GoalStatus.Achieved);
    }

    public class AddTherapyGoalRequest
    {
        public Guid GoalCategoryId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? TargetDate { get; set; }
        public string? Notes { get; set; }
    }

    public class UpdateTherapyGoalRequest
    {
        public Guid? GoalCategoryId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? TargetDate { get; set; }
        public GoalStatus? Status { get; set; }
        public string? Notes { get; set; }
    }

}
