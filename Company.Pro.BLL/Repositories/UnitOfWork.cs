using Company.Pro.BLL.Interfaces;
using Company.Pro.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Pro.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        // To Hold The Instance Of DbContext
        private readonly CompanyDbContext _context;

        // Constructor To Initialize The DbContext Instance And Repositories
        public UnitOfWork(CompanyDbContext context)
        {
            _context = context; // Initialize DbContext
            DepartmentRepository = new DepartmentReository(_context); // Initialize Department Repository
            EmployeeRepository = new EmployeeRepository(_context); // Initialize Employee Repository
        }

        public IDepartmentRepository DepartmentRepository { get; } // Property To Access Department Repository

        public IEmployeeRepository EmployeeRepository { get; } // Property To Access Employee Repository

        public async Task<int> CompleteSaveChangesAsync()
        {
            return await _context.SaveChangesAsync(); // Save Changes To Database
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync(); // Dispose DbContext Asynchronously
        }
    }
}
