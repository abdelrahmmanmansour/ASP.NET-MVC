using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Pro.DAL.Models
{
    // Represents a Department within the company
    public class Department : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }

        #region Navigational Property
        // Navigation property to represent the one-to-many relationship with Employees
        public List<Employee> Employees { get; set; }
        #endregion
    }
}
