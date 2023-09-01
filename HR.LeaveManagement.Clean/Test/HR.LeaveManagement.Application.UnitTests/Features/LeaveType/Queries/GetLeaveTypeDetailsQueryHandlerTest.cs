using AutoMapper;
using Castle.Core.Logging;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.leaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.leaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveType.Queries
{
    public class GetLeaveTypeDetailsQueryHandlerTest
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private IMapper _mapper;
        private Mock<IAppLogging<GetLeaveTypeDetailsQueryHandler>> _logger;

        public GetLeaveTypeDetailsQueryHandlerTest()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeMockRepository();
            var mapperConfig = new MapperConfiguration(a =>
            {
                a.AddProfile<LeaveTypeProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<IAppLogging<GetLeaveTypeDetailsQueryHandler>>();
        }



        [Fact]
        public async Task LeaveTypeDetailsQueryHandler_GetAsync_ReturnsOk()
        {
            var handler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object);

            var result = await handler.Handle(new GetLeaveTypeDetailsQuery(5), CancellationToken.None);

            result.ShouldBeOfType<LeaveTypeDetailsDto>();
        }


    }


}
