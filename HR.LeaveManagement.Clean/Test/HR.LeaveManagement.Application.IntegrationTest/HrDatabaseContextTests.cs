using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.IntegrationTest
{
    public class HrDatabaseContextTests
    {
        private HrDatabaseContext _context;

        public HrDatabaseContextTests()
        {
            var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _context = new HrDatabaseContext(dbOptions);
        }

        [Fact]
        public async Task Save_SetCreatedDate_Done()
        {
            // arrange
            var leaveType = new LeaveType()
            {
                Id = 1,
                Name = "test",
                DefaultDays = 5,
            };

            //act
            await _context.AddAsync(leaveType);
            await _context.SaveChangesAsync();

            //assert

            leaveType.DateCreated.ShouldNotBeNull();    
        }



        [Fact]
        public async Task Save_SetModifiedDate_Done()
        {
            // arrange
            var leaveType = new LeaveType()
            {
                Id = 1,
                Name = "test",
                DefaultDays = 5,
            };

            //act
            await _context.AddAsync(leaveType);
            await _context.SaveChangesAsync();

            //assert

            leaveType.DateModified.ShouldNotBeNull();
        }
    }
}
