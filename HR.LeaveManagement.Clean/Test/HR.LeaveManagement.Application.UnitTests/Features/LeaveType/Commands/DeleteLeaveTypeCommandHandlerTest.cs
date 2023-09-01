using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.leaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveType.Commands
{
    public class DeleteLeaveTypeCommandHandlerTest 
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;

        public DeleteLeaveTypeCommandHandlerTest()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeMockRepository();
        }

        [Fact]

        public async Task DeleteLeaveTypeCommandHandler_ValidCommand_ReturnsUnit()
        {
            // arrange
            var handler =new DeleteLeaveTypeCommandHandler(_mockRepo.Object);
            var command = new DeleteLeaveTypeCommand
            {
                Id = 5,
            };

            var result = await handler.Handle(command, CancellationToken.None);

              result.ShouldBeOfType<Unit>();
            _mockRepo.Verify(repo => repo.DeleteAsync(It.Is<Domain.LeaveType>(lt => lt.Id == command.Id)), Times.Once);

        }

        [Fact]
        public async Task Handle_RecordDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var handler = new DeleteLeaveTypeCommandHandler(_mockRepo.Object);
            var command = new DeleteLeaveTypeCommand
            {
                Id = 1000 // Assuming a LeaveType with this ID doesn't exist in our mock repo
            };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
            _mockRepo.Verify(repo => repo.DeleteAsync(It.IsAny<Domain.LeaveType>()), Times.Never); // Ensure that DeleteAsync was never called
        }
    }
}
