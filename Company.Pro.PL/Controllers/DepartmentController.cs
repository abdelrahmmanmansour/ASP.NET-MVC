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
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };
                var count = _departmentReository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id,string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest("Invalid Id");
            }
            var department = _departmentReository.Get(id.Value); // id.Value => because id is nullable
            if (department is null)
            {
                return NotFound("Department Not Found");
            }
            return View(ViewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest("Invalid Id");
            }
            var department = _departmentReository.Get(id.Value); // id.Value => because id is nullable
            if (department is null)
            {
                return NotFound("Department Not Found");
            }
            var departmentDto = new CreateDepartmentDto()
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt
            };
            return View(departmentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // To Prevent Any Tools From Sending Request To This Action Method
        public IActionResult Edit([FromRoute] int id, CreateDepartmentDto model) // Model Binding FromRoute To Prevent Overposting Attack
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Id = id,
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };
                var count = _departmentReository.Update(department);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // To Prevent Any Tools From Sending Request To This Action Method
        public IActionResult Delete([FromRoute] int id, Department department) // Model Binding FromRoute To Prevent Overposting Attack
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id)
                {
                    return BadRequest("Invalid Id");
                }
                var count = _departmentReository.Delete(department);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(department);
        }
    }
}
