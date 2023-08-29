using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.leaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IMapper mapper;

        public CreateLeaveAllocationCommandHandler( ILeaveAllocationRepository leaveAllocationRepository , IMapper mapper )
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.mapper = mapper;
        }
        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            // 1 - validate incoming data
            var Validator = new CreateLeaveAllocationValidator(leaveAllocationRepository);
            var validationResult = Validator.ValidateAsync(request);
            if (!validationResult.Result.IsValid)
                throw new BadRequestException("Invalid LeaveAllocation", validationResult);

            // 2- convert to domain object

            var LeaveAllocationToCreate = mapper.Map<Domain.LeaveAllocation>(request);

            // get employee

            // get peroid

            // 3- add to db
            await leaveAllocationRepository.CreateAsync(LeaveAllocationToCreate);

            // 4 - return record id
            return LeaveAllocationToCreate.Id;
        }

    }
}
