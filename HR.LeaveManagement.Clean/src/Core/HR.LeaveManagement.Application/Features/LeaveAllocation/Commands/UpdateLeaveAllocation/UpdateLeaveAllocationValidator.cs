using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.leaveType.Commands.UpdateLeaveType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationValidator : AbstractValidator<UpdateLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public UpdateLeaveAllocationValidator(ILeaveAllocationRepository leaveAllocationRepository , ILeaveTypeRepository leaveTypeRepository) 
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.leaveTypeRepository = leaveTypeRepository;

            // leaveTypeMustExits if false the message will appear
            RuleFor(p => p.Id).NotNull()
                .MustAsync(leaveAllocationMustExits).WithMessage("LeaveAllocation must Exist");

            RuleFor(p => p.Peroid)
                .NotEmpty().WithMessage("Name should not be Empty")
                .GreaterThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("{ PropertyName } be less than {ComparisonValue}");

            RuleFor(p => p.NumbersOfDays)
                .GreaterThan(0).WithMessage("{PropertyName} should not be greater than 0")
                .LessThan(100)
               .WithMessage("{PropertyName} should be greater than 100");


            RuleFor(p => p.LeaveTypeId)
              .GreaterThan(0).WithMessage("{PropertyName} should not be greater than 0")
              .LessThan(100)
             .WithMessage("{PropertyName} should be greater than 100")
             .MustAsync(leaveTypeMustExits).WithMessage("LeaveType must Exist");
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
       private async Task<bool> leaveTypeMustExits(int id, CancellationToken token)
       {
            var res = await leaveTypeRepository.GetAsync(id);
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
