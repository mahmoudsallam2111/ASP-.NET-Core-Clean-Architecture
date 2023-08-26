using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveType.Queries.GetLeaveTypeDetails
{
    public class LeaveTypeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DefaultDays { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
