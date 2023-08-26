using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation?> GetLeaveAllocationWithDetails(int id);
    Task<List<LeaveAllocation>> GetAllLeaveAllocationWithDetails();
    Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId);
    Task<LeaveAllocation?> GetUserLeaveAllocation(string userId, int leavetypeid);
    Task<bool> AllocationExists(string userId , int leavetypeid , int peroid);
    Task AddAllocations(List<LeaveAllocation> leaveAllocations);

}
