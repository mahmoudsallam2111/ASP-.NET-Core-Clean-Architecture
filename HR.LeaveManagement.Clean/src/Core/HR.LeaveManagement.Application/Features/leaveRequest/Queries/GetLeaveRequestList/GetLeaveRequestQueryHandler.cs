using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestQueryHandler : IRequestHandler<GetLeaveRequestQuery, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;
        private readonly IAppLogging<GetLeaveRequestQueryHandler> logger;

        public GetLeaveRequestQueryHandler(ILeaveRequestRepository leaveRequestRepository ,
            IMapper mapper ,
            IAppLogging<GetLeaveRequestQueryHandler> logger)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestQuery request, CancellationToken cancellationToken)
        {
            var leaveRequestFromDb = await leaveRequestRepository.GetAllAsync();

            logger.LoggingInformation("leave requests retrived succesfully"); 

            return mapper.Map<List<LeaveRequestListDto>>(leaveRequestFromDb);
        }
    }
}
