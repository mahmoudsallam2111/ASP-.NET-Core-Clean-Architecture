using AutoMapper;
using HR.LeaveManagement.Application.Features.leaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.leaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.leaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Application.Features.leaveRequest.Queries.GetLeaveRequestList;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<LeaveRequest , LeaveRequestListDto>().ReverseMap();
            CreateMap<LeaveRequest , LeaveRequestDetailsDto>().ReverseMap();
            CreateMap<CreateLeaveRequestCommand , LeaveRequest>();
            CreateMap<UpdateLeaveRequestCommand , LeaveRequest>();
        }
    }
}
