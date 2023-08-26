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
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name should not be Empty")
                .NotNull()
                .MaximumLength(100).WithMessage("Name must be less than 100 char");

            RuleFor(p => p.DefaultDays)
                .GreaterThan(1).WithMessage("{PropertyName} should not be greater than 100")
                .LessThan(100)
               .WithMessage("{PropertyName} should be greater than 100");

            RuleFor(p => p)
                .MustAsync(LeaveTypeNameUnique).WithMessage("LeaveType Name must be Unique");
            this.leaveTypeRepository = leaveTypeRepository;
        }

        private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
        {
            return leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
    }
}
