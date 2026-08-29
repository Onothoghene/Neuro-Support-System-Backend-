using Domain.Common;

namespace Domain.Entities
{
    public class SessionDuration : BaseEntity
    {
        public required string Label { get; set; }      // e.g. "30 minutes"
        public int Minutes { get; set; }       // e.g. 30
        public bool IsActive { get; set; } = true;
    }
}
