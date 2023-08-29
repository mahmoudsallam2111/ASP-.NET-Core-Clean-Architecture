using HR.LeaveManagement.Application.Features.leaveType.Queries.GetAllLeaveTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveRequest.Queries.GetLeaveRequestDetails
{
    public class LeaveRequestDetailsDto
    {
        public string RequestingEmployeeId { get; set; } = string.Empty;
        public LeaveTypeDto? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequests { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? RequestComments { get; set; }
        public bool? Approved { get; set; }
        public bool? Cancelled { get; set; }
    }
}
