using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.leaveType.Queries.GetAllLeaveTypes;
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
    public class GetLeaveTypeQueryHandlerTest
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private IMapper _mapper;
        private Mock<IAppLogging<GetLeaveTypeQueryHandler>> _logger;

        public GetLeaveTypeQueryHandlerTest()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeMockRepository();
            var mapperConfig = new MapperConfiguration(a =>
            {
                a.AddProfile<LeaveTypeProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _logger = new Mock<IAppLogging<GetLeaveTypeQueryHandler>>();
        }

        [Fact]
        public async Task LeaveTypeQueryHandler_GetAll_ReturnsOk()
        {
            var handler = new GetLeaveTypeQueryHandler(_mapper, _mockRepo.Object , _logger.Object);

            var result  = await handler.Handle(new GetLeaveTypeQuery() , CancellationToken.None);

            result.ShouldBeOfType<List<LeaveTypeDto>>();
            result.Count.ShouldBe(3);
        }
    }
}
