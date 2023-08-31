using HR.LeaveManagement.Application.Features.leaveRequest.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommand : BaseLeaveRequest , IRequest<int>
    {
        public string RequestComments { get; set; } = string.Empty; 
    }
}
