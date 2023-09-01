using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDatabaseContext db) : base(db)
        {
        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            // true == false    false
            // false == false    true
            return await db.leaveTypes.AnyAsync(x => x.Name == name) == false;
        }

        public  async Task<bool> leaveTypeMustExits(int id)
        {
            return await db.leaveTypes.AnyAsync(a=>a.Id == id); 
           
        }
    }
}
