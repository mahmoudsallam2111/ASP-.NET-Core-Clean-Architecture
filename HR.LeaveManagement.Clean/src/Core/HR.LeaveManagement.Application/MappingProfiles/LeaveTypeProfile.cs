using AutoMapper;
using HR.LeaveManagement.Application.Features.leaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.leaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.leaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.leaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveTypeProfile:Profile
    {
       public LeaveTypeProfile() 
        {
            CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();          
            CreateMap<LeaveType, LeaveTypeDetailsDto>().ReverseMap();          
            CreateMap<LeaveType, CreateLeaveTypeCommand>().ReverseMap();          
            CreateMap<LeaveType, UpdateLeaveTypeCommand>().ReverseMap();          
        }
    }
}
