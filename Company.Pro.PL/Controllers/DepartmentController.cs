using Company.Pro.BLL.Interfaces;
using Company.Pro.BLL.Repositories;
using Company.Pro.DAL.Models;
using Company.Pro.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Pro.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentReository;  // Null

        // Ask CLR TO Create Object from DepartmentReository Class
        // Dependency Injection 
        public DepartmentController(IDepartmentRepository departmentReository)
        {
            _departmentReository = departmentReository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            // Get all departments from the database
            var departments = _departmentReository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Display the create department form
            // Add Department
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            // Display the create department form
            // Add Department
            if(ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };
                var count = _departmentReository.Add(department);
                if(count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}
