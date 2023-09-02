using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Domain.Common
{
    public abstract class BaseEntity<T>
    {
          public required T Id { get; set; }
          public DateTime? DateCreated { get; set; }
          public string? CreatedBy { get; set; }
          public DateTime? DateModified { get; set; }
          public string? ModifiedBy { get; set; }

    }
}
