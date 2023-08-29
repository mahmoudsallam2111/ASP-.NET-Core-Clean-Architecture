using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationValidator : AbstractValidator<CreateLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        public CreateLeaveAllocationValidator(ILeaveAllocationRepository leaveAllocationRepository) 
        {
            this.leaveAllocationRepository = leaveAllocationRepository;

            RuleFor(a => a.LeaveTypeId).GreaterThan(0).WithMessage("LeaveTypeId must be grater than 0")
                .MustAsync(leaveAllocationMustExits).WithMessage("LeaveTypeId must exist");
        }


        private async Task<bool> leaveAllocationMustExits(int id, CancellationToken token)
        {
            var res = await leaveAllocationRepository.GetAsync(id);
            if (res == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
