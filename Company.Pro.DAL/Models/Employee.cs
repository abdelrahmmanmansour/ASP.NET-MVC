using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Pro.DAL.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }

        [Range(20, 60)]
        public int? Age { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s,.-]{1,200}$")]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime CreateAt { get; set; }

        #region Navigational Property
        // Navigation property to represent the one-to-many relationship with Employees
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        #endregion

        public string? ImageName { get; set; } // For Storing Image Name in Database
    }
}
