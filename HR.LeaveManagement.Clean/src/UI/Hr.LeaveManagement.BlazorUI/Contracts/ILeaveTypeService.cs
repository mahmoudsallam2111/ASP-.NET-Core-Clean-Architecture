using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Hr.LeaveManagement.BlazorUI.Services.Base;

namespace Hr.LeaveManagement.BlazorUI.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVM>> GetAll();
        Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
        Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveTypeVM);
        Task<Response<Guid>> UpdateLeaveType(LeaveTypeVM leaveTypeVM);
        Task<Response<Guid>> DeleteLeaveType(int id);

    }
}
