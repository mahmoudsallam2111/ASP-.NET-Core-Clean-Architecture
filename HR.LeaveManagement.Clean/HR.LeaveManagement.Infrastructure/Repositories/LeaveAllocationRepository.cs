using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Infrastructure.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext db) : base(db)
        {
        }

        public  async Task AddAllocations(List<LeaveAllocation> leaveAllocations)
        {
            //  this is usualy used to add a collection
          await db.leaveAllocations.AddRangeAsync(leaveAllocations); 
          await db.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leavetypeid, int peroid)
        {
            return await db.leaveAllocations.AnyAsync(a => a.EmployeeId == userId && a.LeaveTypeId == leavetypeid
            && a.Peroid == peroid
            );
        }

        public async Task<List<LeaveAllocation>> GetAllLeaveAllocationWithDetails()
        {
            return await db.leaveAllocations
                .Include(a => a.LeaveType)
                .ToListAsync();
        }

        public async Task<LeaveAllocation?> GetLeaveAllocationWithDetails(int id)
        {
            return await db.leaveAllocations
               .Include(l => l.LeaveType)
               .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
        {

            return await db.leaveAllocations
                .Where(u => u.EmployeeId == userId)
                .Include(l => l.LeaveType)
                .ToListAsync();
        }

        public async Task<LeaveAllocation?> GetUserLeaveAllocation(string userId, int leavetypeid)
        {
            return await db.leaveAllocations
                .FirstOrDefaultAsync(a => a.EmployeeId == userId
                && a.LeaveTypeId == leavetypeid);
        }
    }
}
