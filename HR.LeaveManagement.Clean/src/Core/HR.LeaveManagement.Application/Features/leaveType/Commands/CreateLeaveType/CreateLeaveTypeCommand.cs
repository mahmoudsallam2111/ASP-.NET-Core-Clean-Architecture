using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommand:IRequest<int>
    {
        // this is the fields i need to create a new instance from LeaveType
        public string Name { get; set; } = string.Empty;
        public int DefaultDays { get; set; }
    }
}
