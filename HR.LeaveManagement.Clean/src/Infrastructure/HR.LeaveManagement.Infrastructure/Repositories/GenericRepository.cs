using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain.Common;
using HR.LeaveManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly HrDatabaseContext db;

        public GenericRepository(HrDatabaseContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(T entity)
        {
            await  db.AddAsync(entity);
           await  db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            db.Remove(entity); 
           await db.SaveChangesAsync();

        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await db.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await db.Set<T>().SingleOrDefaultAsync(a=>a.Id ==id);
        }

        public async Task UpdateAsync(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
           await db.SaveChangesAsync();
        }
    }
}
