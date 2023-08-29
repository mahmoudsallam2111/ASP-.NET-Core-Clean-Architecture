using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveRequest.Commands.DeleteLeaveRequest
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IAppLogging<DeleteLeaveRequestCommandHandler> logger;

        public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository , IAppLogging<DeleteLeaveRequestCommandHandler> logger)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.logger = logger;
        }
        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequestToDelete = await leaveRequestRepository.GetAsync(request.Id);

            if (leaveRequestToDelete == null)
            {
                logger.LoggingWarning("can not remove this items cause i does not exist");
                throw new NotFoundException(nameof(leaveRequest)  ,  request.Id);
            }

          await  leaveRequestRepository.DeleteAsync(leaveRequestToDelete);

            return Unit.Value;

        }
    }
}
