using Company.Pro.BLL.Interfaces;
using Company.Pro.DAL.Data.Contexts;
using Company.Pro.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Pro.BLL.Repositories
{
    // Implementation of the IEmployeeRepository interface
    // I need to all method => connect to Database
    // so make object from CompanyDbContext globally
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository //  To Inherit All Method From Generic Repository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _context = context;
        }

        // Method To Search Employees By Name
        public List<Employee> GetEmployeeByName(string name)
        {
            // Include Department Data Using Eager Loading
            return _context.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToList();
        }









        ////  readonly to make sure that the context is not changed after initialization
        //private readonly CompanyDbContext _context; // Null

        //// Asks CLR to create an object from CompanyDbContext class
        //public EmployeeRepository(CompanyDbContext companyDbContext)
        //{
        //    _context = companyDbContext;
        //}
        //public IEnumerable<Employee> GetAll()
        //{
        //    return _context.Employees.ToList();
        //}

        //public Employee? Get(int id)
        //{
        //    return _context.Employees.Find(id);
        //}

        //public int Add(Employee employee)
        //{
        //    _context.Employees.Add(employee);
        //    return _context.SaveChanges();
        //}

        //public int Update(Employee employee)
        //{
        //    _context.Employees.Update(employee);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    _context.Employees.Remove(employee);
        //    return _context.SaveChanges();
        //}
    }
}
