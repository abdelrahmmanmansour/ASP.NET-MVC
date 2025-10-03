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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;

        public GenericRepository(CompanyDbContext companyDbContext)
        {
            _context = companyDbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Employee))
            {
                // Eager Loading
                return (IEnumerable<T>) await _context.Employees.Include(E => E.Department).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T model)
        {
          await _context.Set<T>().AddAsync(model);
            //return _context.SaveChanges();
        }

        public void Update(T model)
        {
            _context.Set<T>().Update(model);
            //return _context.SaveChanges();
        }

        public void Delete(T model)
        {
            _context.Set<T>().Remove(model);
            //return _context.SaveChanges();
        }
    }
}
