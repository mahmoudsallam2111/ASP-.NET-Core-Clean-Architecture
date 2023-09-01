using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.leaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using MediatR;
using Moq;
using Shouldly;


namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveType.Commands
{
    public class UpdateLeaveTypeCommandHandlerTest
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandlerTest()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeMockRepository();
            var mapperConfig = new MapperConfiguration(a =>
            {
                a.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task UpdateLeaveTypeCommandHandler_ValidCommand_ReturnsUnit()
        {
            // Arrange
            var handler = new UpdateLeaveTypeCommandHandler(_mapper, _mockRepo.Object );
            var command = new UpdateLeaveTypeCommand
            {
                // Populate with valid data

                Id = 1,
                Name = "Testttttt",  
                DefaultDays = 5,
            };

         

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBe(Unit.Value);
            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.LeaveType>()), Times.Once);
        }

        [Fact]
        public async Task UpdateLeaveTypeCommandHandler_InvalidCommand_ThrowsBadRequestException()
        {
            // Arrange
            var handler = new UpdateLeaveTypeCommandHandler(_mapper, _mockRepo.Object);
            var command = new UpdateLeaveTypeCommand
            {
                // Populate with invalid data
                Name = "Test",
                DefaultDays = 10000,
            };
           

            // act && Assert
            await Should.ThrowAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
            _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.LeaveType>()), Times.Never);
        }


    }
}
