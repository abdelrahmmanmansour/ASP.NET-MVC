using Company.Pro.BLL.Interfaces;
using Company.Pro.DAL.Models;
using Company.Pro.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Pro.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;  // Null

        // Ask CLR TO Create Object from DepartmentReository Class
        // Dependency Injection 
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            // Get all employess from the database
            var employees= _employeeRepository.GetAll();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Display the create department form
            // Add Department
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            // Display the create department form
            // Add Department
            if (ModelState.IsValid)  // Server Side Validation
            {
                var employee = new Employee()
                {
                   Name = model.Name,
                   Age = model.Age,
                   Email = model.Email,
                   Address = model.Address,
                   Phone = model.Phone,
                   Salary = model.Salary,
                   IsActive = model.IsActive,
                   IsDeleted = model.IsDeleted,
                   HiringDate = model.HiringDate,
                   CreateAt = model.CreateAt
                };
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest("Invalid Id");
            }
            var employee = _employeeRepository.Get(id.Value); // id.Value => because id is nullable
            if (employee is null)
            {
                return NotFound("Employee Not Found");
            }
            return View(ViewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest("Invalid Id");
            }
            var employee = _employeeRepository.Get(id.Value); // id.Value => because id is nullable
            if (employee is null)
            {
                return NotFound("Employee Not Found");
            }
            var employeeDto = new CreateEmployeeDto()
            {
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Address = employee.Address,
                Phone = employee.Phone,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                IsDeleted = employee.IsDeleted,
                HiringDate = employee.HiringDate,
                CreateAt = employee.CreateAt
            };
            return View(employeeDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // To Prevent Any Tools From Sending Request To This Action Method
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model) // Model Binding FromRoute To Prevent Overposting Attack
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Id = id,
                    Name = model.Name,
                    Age = model.Age,
                    Email = model.Email,
                    Address = model.Address,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    HiringDate = model.HiringDate,
                    CreateAt = model.CreateAt
                };
                var count = _employeeRepository.Update(employee);
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
        public IActionResult Delete([FromRoute] int id, Employee employee) // Model Binding FromRoute To Prevent Overposting Attack
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id)
                {
                    return BadRequest("Invalid Id");
                }
                var count = _employeeRepository.Delete(employee);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }
    }
}
