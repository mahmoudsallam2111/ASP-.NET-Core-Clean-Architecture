using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandValidator:AbstractValidator<UpdateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;

            // leaveTypeMustExits if false the message will appear
            RuleFor(p => p.Id).NotNull()
                .MustAsync(leaveTypeMustExits) .WithMessage("LeaveType must Exist");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name should not be Empty")
                .NotNull()
                .MaximumLength(100).WithMessage("Name must be less than 100 char");

            RuleFor(p => p.DefaultDays)
                .GreaterThan(100).WithMessage("{PropertyName} should not be greater than 100")
                .LessThan(1)
               .WithMessage("{PropertyName} should be greater than 100");

            RuleFor(p => p)
                .MustAsync(LeaveTypeNameUnique).WithMessage("LeaveType Name must be Unique");
        }

        private async Task<bool> leaveTypeMustExits(int id, CancellationToken token)
        {
           var res = await leaveTypeRepository.GetAsync(id);
            if (res == null)
            {
              return  false;
            }
            else
            {
                return true;
            }

        }

        private Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken token)
        {
            return leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
    }
}
