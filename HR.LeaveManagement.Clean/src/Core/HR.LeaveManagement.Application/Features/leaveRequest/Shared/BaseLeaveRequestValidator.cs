using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveRequest.Shared
{
    public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(a => a.StartDate)
                .LessThan(a => a.EndDate)
                .WithMessage("{propertyName} must be before {ComparisonValue}");

            RuleFor(a => a.EndDate)
               .GreaterThan(a => a.StartDate)
               .WithMessage("{propertyName} must be after {ComparisonValue}");

            RuleFor(a => a.LeaveTypeId)
           .GreaterThan(0)
           .WithMessage("{propertyName} must be after {ComparisonValue}")
           .MustAsync(leaveTypeMustExits).WithMessage("LeaveType must Exist");
            this.leaveTypeRepository = leaveTypeRepository;
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
