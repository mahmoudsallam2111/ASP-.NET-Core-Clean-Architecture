﻿using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest?> GetLeaveRequestWithDetails(int id);

    Task<List<LeaveRequest>> GetAllLeaveRequestWithDetails();
    Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string userId);
}
