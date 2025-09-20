using Company.Pro.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Pro.BLL.Interfaces
{
    // Interface for Department repository
    // This interface can be expanded with method signatures for CRUD operations
    // Signuture Of Methods 
    public interface IDepartmentRepository
    {
        // Method to get all Department => Sequence of Department
        IEnumerable<Department> GetAll();

        // Method to get a Department by its ID => Department or null if not found
        Department? Get(int id);

        // Method to add a new Department => int (ID of the newly added Department)
        // integer to See Save Changes 
        int Add(Department department);

        // Method to update an existing Department => int (number of records affected)
        // integer to See Save Changes
        int Update(Department department);

        // Method to delete a Department => int (number of records affected)
        // integer to See Save Changes
        int Delete(Department department);
    }
}
