using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Infrastructure.DatabaseContext;
using HR.LeaveManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure
{
    /// <summary>
    /// here i configure the db && register repositories
    /// </summary>
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services , IConfiguration configuration)
        {   
            // configure db
            services.AddDbContext<HrDatabaseContext>(options =>
            {
                 options.UseSqlServer(configuration.GetConnectionString("defaultConnectionString"));
            });

            // register repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(IGenericRepository<>));
            services.AddScoped<ILeaveTypeRepository , LeaveTypeRepository>();
            services.AddScoped<ILeaveRequestRepository , LeaveRequestRepository>();
            services.AddScoped<ILeaveAllocationRepository , LeaveAllocationRepository>();

            return services;
        }
    }
}
