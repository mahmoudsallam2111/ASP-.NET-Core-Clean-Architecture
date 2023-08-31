using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;
        private readonly IAppLogging<CreateLeaveRequestCommandHandler> logger;
        private readonly IEmailSender emailSender;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository ,
            ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper ,
            IAppLogging<CreateLeaveRequestCommandHandler> logger , 
            IEmailSender emailSender)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.emailSender = emailSender;
        }
        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            // 1 - validate incoming data
            var Validator = new CreateLeaveRequestCommandValidator(leaveTypeRepository);
            var validationResult = Validator.ValidateAsync(request);
            if (!validationResult.Result.IsValid)
                throw new BadRequestException("Invalid LeaveRequest", validationResult);

            // 2- convert to domain object

            var LeaveRequestToCreate = mapper.Map<Domain.LeaveRequest>(request);

            // 3- add to db
            await leaveRequestRepository.CreateAsync(LeaveRequestToCreate);

            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty,
                    Subject = "leave request created",
                    Body = $"your leave request for {request.StartDate:D}" +
                           $" to {request.EndDate:D} has been submitted successfully"
                };

                await emailSender.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                logger.LoggingWarning(ex.Message);

            }

            // 4 - return record id
            return LeaveRequestToCreate.Id;
        }
    }
}
