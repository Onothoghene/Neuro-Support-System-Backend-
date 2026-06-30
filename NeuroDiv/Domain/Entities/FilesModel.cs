using Domain.Common;
using System;

namespace Domain.Entities
{
    public class FileModel : AuditableBaseEntity
    {
       
        public UserProfile? UploadedByUser { get; set; }
    }
}
