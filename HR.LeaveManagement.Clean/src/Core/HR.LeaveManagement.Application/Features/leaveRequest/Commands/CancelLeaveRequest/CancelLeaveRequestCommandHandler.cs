using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveRequest.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IEmailSender emailSender;
        private readonly IAppLogging<CancelLeaveRequestCommandHandler> logger;

        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository ,
            IEmailSender emailSender ,
            IAppLogging<CancelLeaveRequestCommandHandler> logger)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.emailSender = emailSender;
            this.logger = logger;
        }
        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequestToCancel = await leaveRequestRepository.GetAsync(request.Id);
            if (leaveRequestToCancel == null)
            {
                throw new NotFoundException(nameof(leaveRequest) , request.Id);
            }

            leaveRequestToCancel.Cancelled = true;

            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty,
                    Subject = "leave request Cancel",
                    Body = $"your leave request for {leaveRequestToCancel.StartDate:D}" +
                           $" to {leaveRequestToCancel.EndDate:D} has been Cancelled successfully"
                };

                await emailSender.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                logger.LoggingWarning(ex.Message);

            }

            return Unit.Value;

        }
    }
}
