using HR.LeaveManagement.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.LeaveManagement.Domain;

public class LeaveRequest: BaseEntity
{
    
    [ForeignKey(nameof(LeaveType))]
    public int LeaveTypeId { get; set; }
    public LeaveType? LeaveType { get; set; }
    public DateTime DateRequests { get; set; }
    public string? RequestComments { get; set; }
    
    public bool? Approved { get; set; }
    public bool? Cancelled { get; set; }


    public string RequestingEmployeeId { get; set; } = string.Empty;    

}
