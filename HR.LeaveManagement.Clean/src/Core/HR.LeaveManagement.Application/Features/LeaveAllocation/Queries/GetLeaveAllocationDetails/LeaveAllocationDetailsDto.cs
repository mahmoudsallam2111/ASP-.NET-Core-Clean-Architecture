using HR.LeaveManagement.Application.Features.leaveType.Queries.GetAllLeaveTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class LeaveAllocationDetailsDto
    {
        public int NumbersOfDays { get; set; }

        public LeaveTypeDto? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        public int Peroid { get; set; }
    }
}
