using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandValidator:AbstractValidator<CreateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} should not be Empty")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must be less than 100 char");

            RuleFor(p => p.DefaultDays)
                .GreaterThan(1).WithMessage("{PropertyName} should not be greater than 100")
                .LessThan(100)
               .WithMessage("{PropertyName} should be greater than 100");

            RuleFor(p => p.Name)
                .MustAsync(LeaveTypeNameUnique).WithMessage("LeaveType Name must be Unique");
        }

        private Task<bool> LeaveTypeNameUnique(string name, CancellationToken token)
        {
            return  leaveTypeRepository.IsLeaveTypeUnique(name);
        }
    }
}
