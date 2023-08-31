using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.leaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveRequest.Commands.ChangeLeaveRequestApprovalCommand
{
    public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand , Unit>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IAppLogging<ChangeLeaveRequestApprovalCommandHandler> logger;
        private readonly IEmailSender emailSender;

        public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository leaveRequestRepository ,
            IAppLogging<ChangeLeaveRequestApprovalCommandHandler> logger ,
            IEmailSender emailSender)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            // 1 - validate incoming data
            var Validator = new ChangeLeaveRequestApprovalCommandValidator();
            var ValidationResult = Validator.ValidateAsync(request);
            if (ValidationResult.Result.Errors.Any())
            {
                throw new BadRequestException("Invalid LeaveType", ValidationResult);
            }
            var leaveRequestToChange = await leaveRequestRepository.GetAsync(request.Id);

            if (leaveRequestToChange == null)  
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            leaveRequestToChange.Approved = request.Approval;
            
           await leaveRequestRepository.UpdateAsync(leaveRequestToChange);


            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty,
                    Subject = "leave request Approval",
                    Body = $"your leave request for {leaveRequestToChange.StartDate:D}" +
                           $" to {leaveRequestToChange.EndDate:D} has been updated successfully"
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
