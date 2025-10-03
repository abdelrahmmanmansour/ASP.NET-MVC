using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Pro.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IDepartmentRepository DepartmentRepository { get; } // Property To Access Department Repository
        IEmployeeRepository EmployeeRepository { get; } // Property To Access Employee Repository

        // Method To Save Changes:

        Task<int> CompleteSaveChangesAsync(); // Method To Save Changes
    }
}
