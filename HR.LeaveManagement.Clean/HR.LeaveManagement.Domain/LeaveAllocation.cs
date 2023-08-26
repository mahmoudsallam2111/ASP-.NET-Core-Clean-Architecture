using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Domain;

public class LeaveAllocation:BaseEntity
{
    public int NumbersOfDays { get; set; }

    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }    

    public int Peroid { get; set; }

}
