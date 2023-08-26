using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveTypeCommandHandler(IMapper mapper , ILeaveTypeRepository leaveTypeRepository)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // 1 - validate incoming data
            var Validator = new CreateLeaveTypeCommandValidator(leaveTypeRepository);
            var validationResult = Validator.ValidateAsync(request);
            if (!validationResult.IsCompletedSuccessfully)
                throw new BadRequestException("Invalid LeaveType" , validationResult);

            // 2- convert to domain object

            var LeaveTypeToCreate = mapper.Map<LeaveType>(request);

            // 3- add to db
           await leaveTypeRepository.CreateAsync(LeaveTypeToCreate);

            // 4 - return record id
            return LeaveTypeToCreate.Id;    
        }
    }
}
