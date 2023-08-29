using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveRequest.Queries.GetLeaveRequestDetails
{
    public class LeaveRequestDetailsQueryHandler : IRequestHandler<LeaveRequestDetailsQuery, LeaveRequestDetailsDto>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;
        private readonly IAppLogging<LeaveRequestDetailsQueryHandler> logger;

        public LeaveRequestDetailsQueryHandler(ILeaveRequestRepository leaveRequestRepository , 
            IMapper mapper , 
            IAppLogging<LeaveRequestDetailsQueryHandler> logger)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<LeaveRequestDetailsDto> Handle(LeaveRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var leaveRequestFromDb = await leaveRequestRepository.GetAsync(request.Id);

            if (leaveRequestFromDb is null)
            {
                logger.LoggingWarning("u are trying to access an object that not exist");
                throw new NotFoundException(nameof(leaveRequest) , request.Id);
            }

            

            logger.LoggingInformation(" leave request is loaded successfully ");
            return mapper.Map<LeaveRequestDetailsDto>(leaveRequestFromDb);  

        }
    }
}
