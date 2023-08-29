using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.leaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository
            , IMapper mapper
            , ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            // 1 - validate incoming data
            var Validator = new UpdateLeaveAllocationValidator(leaveAllocationRepository , leaveTypeRepository);
            var ValidationResult = Validator.ValidateAsync(request);
            if (ValidationResult.Result.Errors.Any())
            {
                throw new BadRequestException("Invalid LeaveType", ValidationResult);
            }
            var leaveAllocationToUpdate = await leaveAllocationRepository.GetAsync(request.Id);

            if (leaveAllocationToUpdate == null)  // this check is not nessacery as i validate that leaveAllocation must exist in validator 
                throw new NotFoundException(nameof(leaveAllocationToUpdate) , request.Id);
            
            // 2- convert to domain object
            var leaveAllocation = mapper.Map<Domain.LeaveAllocation>(request);

            // 3- update to db
            await leaveAllocationRepository.UpdateAsync(leaveAllocation);

            // 4 - return unit value

            return Unit.Value;
        }
    }
}
