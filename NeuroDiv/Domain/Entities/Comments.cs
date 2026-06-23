using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Comments : AuditableBaseEntity
    {
        public string CommentText { get; set; }
        public double Rating { get; set; }

        public UserProfile CreatedByNavigation { get; set; }
    }
}
