using Application.DTOs.CommonNodes;
using Domain.Enums;
using System;

namespace Application.DTOs.ChildTherapistAssignment
{
    public class ChildTherapistAssignmentVM : AuditableBaseEntityVM 
    {
        //public Guid ChildId { get; set; }
        //public Guid TherapistId { get; set; }
        //public string AssignmentDetails { get; set; }

        public Guid AssignmentId { get; set; }
        public Guid TherapistId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AssignmentRole { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class AssignTherapistRequest
    {
        public Guid ChildProfileId { get; set; }
        public Guid TherapistId { get; set; }
        public AssignmentRole Role { get; set; } = AssignmentRole.Primary;
    }


}
