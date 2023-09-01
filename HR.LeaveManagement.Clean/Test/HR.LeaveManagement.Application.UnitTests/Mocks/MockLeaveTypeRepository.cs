using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.leaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetLeaveTypeMockRepository()
        {
            var leaveTypes = new List<LeaveType>()
            {
                new LeaveType { Id = 1, DefaultDays = 10  , Name = "test1"},
                new LeaveType { Id = 2, DefaultDays = 11  , Name = "test2"},
                new LeaveType { Id = 3, DefaultDays = 12  , Name = "test3"},
            };

            var leaveType = new LeaveType() { Id = 5, DefaultDays = 7, Name = "test" };

            // initiate mock object of ILeaveTypeRepository
            var MockRepo = new Mock<ILeaveTypeRepository>();

            // setup getall method
            MockRepo.Setup(a => a.GetAllAsync())
                .ReturnsAsync(leaveTypes);

            // setup get method
            MockRepo.Setup(a => a.GetAsync(5))
                .ReturnsAsync(leaveType);

            // setup craete method
            MockRepo.Setup(a => a.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    leaveTypes.Add(leaveType);  // this leave type is added to the list
                    return Task.CompletedTask;

                });

            // setup is unique method
            MockRepo.Setup(x => x.IsLeaveTypeUnique(It.IsAny<string>())).ReturnsAsync(true);

            MockRepo.Setup(x => x.leaveTypeMustExits(It.IsAny<int>())).ReturnsAsync(true);


            // setup is update method
            MockRepo.Setup(m => m.UpdateAsync(It.IsAny<LeaveType>()))
                 .Callback<LeaveType>((updatedLeaveType) =>
                  {
                      var index = leaveTypes.FindIndex(lt => lt.Id == updatedLeaveType.Id);
                      if (index != -1)
                      {
                          leaveTypes[index] = updatedLeaveType;
                      }
                  });


            // setup delete method
            MockRepo.Setup(m => m.DeleteAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    leaveTypes.Remove(leaveType);
                    return Task.CompletedTask;
                });



            return MockRepo;

        }
    }
}
