using AutoMapper;
using Castle.Core.Logging;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.leaveType.Commands.CreateLeaveType;
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

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveType.Commands
{
    public class CreateLeaveTypeCommandHandlerTest
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private IMapper _mapper;

        public CreateLeaveTypeCommandHandlerTest()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeMockRepository();
            var mapperConfig = new MapperConfiguration(a =>
            {
                a.AddProfile<LeaveTypeProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task CreateLeaveTypeCommandHandler_ValidCommand_ReturnsInt()
        {
            // Arrange
            var handler = new CreateLeaveTypeCommandHandler(_mapper, _mockRepo.Object);
            var command = new CreateLeaveTypeCommand
            {
                // Populate the command properties here, assuming it's valid
                Name = "hello",
                DefaultDays = 5,
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<int>();  // This asserts that the result is of type int
            _mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Domain.LeaveType>()), Times.Once); // Ensure that the CreateAsync method is called once
        }

        [Fact]
        public async Task CreateLeaveTypeCommandHandler_InvalidCommand_ThrowsBadRequestException()
        {
            // Arrange
            var handler = new CreateLeaveTypeCommandHandler(_mapper, _mockRepo.Object);
            var command = new CreateLeaveTypeCommand
            {
                // Populate the command properties with invalid data here
                Name = "Name",
            };

            // Act & Assert
            await Should.ThrowAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
            _mockRepo.Verify(repo => repo.CreateAsync(It.IsAny<Domain.LeaveType>()), Times.Never); // Ensure that the CreateAsync method is never called
        }

    }
}
