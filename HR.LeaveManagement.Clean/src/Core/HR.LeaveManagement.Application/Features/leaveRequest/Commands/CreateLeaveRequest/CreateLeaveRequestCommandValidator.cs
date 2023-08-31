using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.leaveRequest.Shared;

namespace HR.LeaveManagement.Application.Features.leaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandValidator : AbstractValidator<CreateLeaveRequestCommand>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            Include(new BaseLeaveRequestValidator(leaveTypeRepository));

            RuleFor(a => a.RequestComments)
                .MinimumLength(500)
                .WithMessage("comment must not exceed 500 chars");
        }
        
    }
}
