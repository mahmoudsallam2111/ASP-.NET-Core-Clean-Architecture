﻿using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.leaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.leaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {

        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;


        public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }

        public UpdateLeaveTypeCommandHandler(object @object, IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // 1 - validate incoming data
            var Validator = new UpdateLeaveTypeCommandValidator(leaveTypeRepository);
            var ValidationResult = Validator.ValidateAsync(request);
            if (ValidationResult.Result.Errors.Any())
            {
                throw new BadRequestException("Invalid LeaveType", ValidationResult);
            }

            // 2- convert to domain object
            var LeaveTypeToUpdate = mapper.Map<LeaveType>(request);

            // 3- update to db
            await leaveTypeRepository.UpdateAsync(LeaveTypeToUpdate);

            // 4 - return unit value

            return Unit.Value;  
        }
    }
}
