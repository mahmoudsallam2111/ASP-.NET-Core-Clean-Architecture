using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;
        private readonly IAppLogging<UpdateLeaveRequestCommandHandler> logger;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository ,
            ILeaveTypeRepository leaveTypeRepository ,
            IMapper mapper ,
            IEmailSender emailSender,
            IAppLogging<UpdateLeaveRequestCommandHandler> logger
            )
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
            this.emailSender = emailSender;
            this.logger = logger;
        }
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {

            // 1 - validate incoming data
            var Validator = new UpdateLeaveRequestCommandValidator(leaveRequestRepository, leaveTypeRepository);
            var ValidationResult = Validator.ValidateAsync(request);
            if (ValidationResult.Result.Errors.Any())
            {
                throw new BadRequestException("Invalid LeaveType", ValidationResult);
            }
            var leaveAllocationToUpdate = await leaveRequestRepository.GetAsync(request.Id);

            if (leaveAllocationToUpdate == null)  // this check is not nessacery as i validate that leaveAllocation must exist in validator 
                throw new NotFoundException(nameof(leaveAllocationToUpdate), request.Id);

            var leaveAllocation = mapper.Map<Domain.LeaveRequest>(request);

            await leaveRequestRepository.UpdateAsync(leaveAllocation);

            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty,
                    Subject = "leave request updated",
                    Body = $"you leave request for {request.StartDate:D}" +
                           $" to {request.EndDate:D} has been updated successfully"
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
