namespace Domain.Enums
{
    public enum CancellationReason
    {
        TherapistUnavailable = 1,
        ChildUnavailable,
        ParentRequest,
        TherapistLeft,
        ScheduleChange,
        Other
    }
}
