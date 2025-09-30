using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Company.Pro.BLL.Interfaces;
using Company.Pro.DAL.Models;
using Company.Pro.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Pro.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;  // Null
        private readonly IDepartmentRepository _departmentRepository; // Null
        private readonly IMapper _mapper;

        // Ask CLR TO Create Object from DepartmentReository Class
        // Dependency Injection 
        public EmployeeController(IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                // Get all employess from the database
                 employees = _employeeRepository.GetAll();
            }
            else
            {
                // Search employees by name
                 employees = _employeeRepository.GetEmployeeByName(SearchInput);
            }
            // Storage in View => Dictionary
            // Has Key And Value
            // Has Three Types Of TempData , ViewData , ViewBag
            // Extra Information
            // 1. ViewData => Transfer Data From Controller(Action) To View Only
            // ViewData["Message"] = "Hi,From ViewData"; // Must Casting
            // 2. ViewBag => Transfer Data From Controller(Action) To View Only // No Need Casting
            // ViewBag.Message = "Hello,From ViewBag";
            // 3. TempData => Transfer Data From Controller(Action) To View And View To View
            // Data => Reqest To Another Request
            // Pass the list of employees to the view
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Display the create department form
            // Add Department
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;  
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            // Display the create department form
            // Add Department
            if (ModelState.IsValid)  // Server Side Validation
            {
                //var employee = new Employee()
                //{
                //   Name = model.Name,
                //   Age = model.Age,
                //   Email = model.Email,
                //   Address = model.Address,
                //   Phone = model.Phone,
                //   Salary = model.Salary,
                //   IsActive = model.IsActive,
                //   IsDeleted = model.IsDeleted,
                //   HiringDate = model.HiringDate,
                //   CreateAt = model.CreateAt,
                //   DepartmentId = model.DepartmentId
                //};
                var employee = _mapper.Map<Employee>(model);
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee Created Successfully"; // TempData To Send Message From Controller To View
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
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            if (id is null)
            {
                return BadRequest("Invalid Id");
            }
            var employee = _employeeRepository.Get(id.Value); // id.Value => because id is nullable
            if (employee is null)
            {
                return NotFound("Employee Not Found");
            }
            //var employeeDto = new CreateEmployeeDto()
            //{
            //    Name = employee.Name,
            //    Age = employee.Age,
            //    Email = employee.Email,
            //    Address = employee.Address,
            //    Phone = employee.Phone,
            //    Salary = employee.Salary,
            //    IsActive = employee.IsActive,
            //    IsDeleted = employee.IsDeleted,
            //    HiringDate = employee.HiringDate,
            //    CreateAt = employee.CreateAt,
            //    DepartmentId = employee.DepartmentId ?? 0 // Handle null case
            //};
            var employeeDto = _mapper.Map<CreateEmployeeDto>(employee);
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
                    CreateAt = model.CreateAt,
                    DepartmentId = model.DepartmentId
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
            //if (ModelState.IsValid) // No Need Because No Input In View
            //{
            if (id != employee.Id)
                {
                    return BadRequest("Invalid Id");
                }
                var count = _employeeRepository.Delete(employee);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
           // }
            return View(employee);
        }
    }
}
