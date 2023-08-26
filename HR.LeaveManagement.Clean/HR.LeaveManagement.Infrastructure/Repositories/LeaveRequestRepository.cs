using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Infrastructure.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HrDatabaseContext db) : base(db)
        {
        }

        public async Task<List<LeaveRequest>> GetAllLeaveRequestWithDetails()
        {
           return await db.leaveRequests
                .Include(a=>a.LeaveType).ToListAsync();  
        }

        public async Task<LeaveRequest?> GetLeaveRequestWithDetails(int id)
        {
            return await db.leaveRequests
                .Include(l => l.LeaveType)
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string userId)
        {

            return await db.leaveRequests
                .Where(u=>u.RequestingEmployeeId == userId)
                .Include(l => l.LeaveType)
                .ToListAsync();
        }
    }
}
