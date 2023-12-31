﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.leaveType.Queries.GetLeaveTypeDetails;

public record GetLeaveTypeDetailsQuery(int Id):IRequest<LeaveTypeDetailsDto>;

