using Company.Pro.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.Pro.DAL.Data.Contexts
{
    // Represents the database context for the Company application
    public class CompanyDbContext : IdentityDbContext<AppUser> // Inherit From IdentityDbContext To Use Identity Features
    {
        #region Notes
        // Default constructor: initializes a new instance of the CompanyDbContext class
        // with default options
        // In a real-world application, consider using dependency injection to pass DbContextOptions 
        #endregion
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        {
            
        }

        // Configures the database connection to use SQL Server with specified connection string
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = .; Database = CompanyMVC; Trusted_Connection = True; TrustServerCertificate = True");
        //}

        // Applies entity configurations from the current assembly
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);  // Call the base method to ensure Identity configurations are applied
        }

        // Represents the Departments table in the database
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
