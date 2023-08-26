using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypeQueryHandler : IRequestHandler<GetLeaveTypeQuery, List<LeaveTypeDto>>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public GetLeaveTypeQueryHandler(IMapper mapper , ILeaveTypeRepository leaveTypeRepository)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeQuery request, CancellationToken cancellationToken)
        {
            // query the db
            var LeaveTypeFromDB = await leaveTypeRepository.GetAllAsync();

            // convert the result to dto
            var data = mapper.Map<List<LeaveTypeDto>>(LeaveTypeFromDB);

            // return thr result
            return data;

        }
    }
}
