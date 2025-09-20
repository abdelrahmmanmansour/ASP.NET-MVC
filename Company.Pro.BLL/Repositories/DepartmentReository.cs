using Company.Pro.BLL.Interfaces;
using Company.Pro.DAL.Data.Contexts;
using Company.Pro.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Pro.BLL.Repositories
{
    // Implementation of the IDepartmentRepository interface
    // I need to all method => connect to Database
    // so make object from CompanyDbContext globally
    public class DepartmentReository : IDepartmentRepository
    {
        //  readonly to make sure that the context is not changed after initialization
        private readonly CompanyDbContext _context; // Null

        // Asks CLR to create an object from CompanyDbContext class
        public DepartmentReository(CompanyDbContext companyDbContext)
        {
            _context = companyDbContext;
        }
        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department? Get(int id)
        {
            return _context.Departments.Find(id);
        }

        public int Add(Department department)
        {
            _context.Departments.Add(department);
            return _context.SaveChanges();
        }

        public int Update(Department department)
        {
            _context.Departments.Update(department);
            return _context.SaveChanges();
        }

        public int Delete(Department department)
        {
            _context.Departments.Remove(department);
            return _context.SaveChanges();
        }
    }
}
