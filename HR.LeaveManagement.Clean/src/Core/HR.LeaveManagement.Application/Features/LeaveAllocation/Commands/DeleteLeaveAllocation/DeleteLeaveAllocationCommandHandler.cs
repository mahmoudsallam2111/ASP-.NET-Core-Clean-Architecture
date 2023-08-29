using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.leaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand , Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            // 1- get domain object from db
            var LeaveAllocationToDelete = await _leaveAllocationRepository.GetAsync(request.Id);

            // verify that recorsd exist
            if (LeaveAllocationToDelete is null)
                throw new NotFoundException(nameof(LeaveAllocationToDelete), request.Id);

            // 2 - delete it
            await _leaveAllocationRepository.DeleteAsync(LeaveAllocationToDelete);

            return Unit.Value;
        }
    }
}
