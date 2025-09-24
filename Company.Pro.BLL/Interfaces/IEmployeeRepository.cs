using Company.Pro.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Pro.BLL.Interfaces
{
    // Interface for Employee repository
    // This interface can be expanded with method signatures for CRUD operations
    // Signuture Of Methods 
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {





        //// Method to get all Employee => Sequence of Employee
        //IEnumerable<Employee> GetAll();

        //// Method to get a Employee by its ID => Employee or null if not found
        //Employee? Get(int id);

        //// Method to add a new Employee => int (ID of the newly added Employee)
        //// integer to See Save Changes 
        //int Add(Employee employee);

        //// Method to update an existing Employee => int (number of records affected)
        //// integer to See Save Changes
        //int Update(Employee employee);

        //// Method to delete a Employee => int (number of records affected)
        //// integer to See Save Changes
        //int Delete(Employee employee);
    }
}
