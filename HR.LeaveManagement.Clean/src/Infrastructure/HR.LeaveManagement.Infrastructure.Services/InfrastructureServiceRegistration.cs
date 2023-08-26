using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagement.Infrastructure.Services.EmailService;
using HR.LeaveManagement.Infrastructure.Services.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure.Services
{
    /// <summary>
    /// here we register third party services
    /// </summary>
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureService(this IServiceCollection services
            , IConfiguration configuration)
        { 
            // configure email services
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped(typeof(IAppLogging<>) , typeof(LoggingAdapter<>));

            return services;
        }
    }
}
