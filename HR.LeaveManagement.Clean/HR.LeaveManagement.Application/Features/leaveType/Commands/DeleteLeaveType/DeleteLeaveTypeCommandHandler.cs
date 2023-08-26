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

namespace HR.LeaveManagement.Application.Features.leaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
    {
        this.leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // 1- get domain object from db
        var LeaveTypeToDelete = await leaveTypeRepository.GetAsync(request.Id);

        // verify that recoed exist
        if (LeaveTypeToDelete is null)
           throw new NotFoundException(nameof(LeaveType) , request.Id);

        // 2 - delete it

        await leaveTypeRepository.DeleteAsync(LeaveTypeToDelete);

        return Unit.Value;
        
    }
}
