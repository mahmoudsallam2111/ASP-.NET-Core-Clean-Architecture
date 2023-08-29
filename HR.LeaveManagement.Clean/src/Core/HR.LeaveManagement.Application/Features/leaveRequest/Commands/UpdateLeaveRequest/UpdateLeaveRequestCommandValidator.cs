using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.leaveRequest.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public UpdateLeaveRequestCommandValidator(ILeaveRequestRepository leaveRequestRepository , ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.leaveTypeRepository = leaveTypeRepository;

            // to contain the validation of the bas class 
            Include(new BaseLeaveRequestValidator(leaveTypeRepository));

            RuleFor(a => a.Id).NotEmpty()
                .MustAsync(leaveRequestMustExits)
                .WithMessage("LeaveRequest Must Exist");

            RuleFor(a => a.RequestComments)
                .MaximumLength(200)
                .WithMessage("Comment must not exceed 200 char");
                
        }
        private async Task<bool> leaveRequestMustExits(int id, CancellationToken token)
        {
            var res = await leaveRequestRepository.GetAsync(id);
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
