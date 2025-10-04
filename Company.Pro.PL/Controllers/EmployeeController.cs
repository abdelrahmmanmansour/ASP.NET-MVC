using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Company.Pro.BLL.Interfaces;
using Company.Pro.DAL.Models;
using Company.Pro.PL.Dtos;
using Company.Pro.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.Pro.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;  // Null
        //private readonly IDepartmentRepository _departmentRepository; // Null
        private readonly IUnitOfWork _unitOfWork; // Null
        private readonly IMapper _mapper;

        // Ask CLR TO Create Object from DepartmentReository Class
        // Dependency Injection 
        public EmployeeController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                // Get all employess from the database
                 employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                // Search employees by name
                 employees = await _unitOfWork.EmployeeRepository.GetEmployeeByNameAsync(SearchInput);
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
        public async Task<IActionResult> Create()
        {
            // Display the create department form
            // Add Department
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;  
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
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

                // Upload Image:
                if (model.Image is not null)
                {
                   model.ImageName =  DocumentSetting.UploadFile(model.Image, "images"); // Call UploadFile Method To Upload Image
                }
                var employee = _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var count = await _unitOfWork.CompleteSaveChangesAsync();
                if (count > 0)
                {
                    TempData["Message"] = "Employee Created Successfully"; // TempData To Send Message From Controller To View
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest("Invalid Id");
            }
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value); // id.Value => because id is nullable
            if (employee is null)
            {
                return NotFound("Employee Not Found");
            }
            return View(ViewName, employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            if (id is null)
            {
                return BadRequest("Invalid Id");
            }
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value); // id.Value => because id is nullable
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
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto model) // Model Binding FromRoute To Prevent Overposting Attack
        {
            if (ModelState.IsValid)
            {

                if (model.ImageName is not null && model.Image is not null) // If User Upload New Image
                {
                    DocumentSetting.DeleteFile(model.ImageName, "images"); // Call DeleteFile Method To Delete Old Image
                }
                // Upload Image:
                if (model.Image is not null)
                {
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images"); // Call UploadFile Method To Upload Image
                }

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
                    DepartmentId = model.DepartmentId,
                    ImageName = model.ImageName
                };

                _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.CompleteSaveChangesAsync();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // To Prevent Any Tools From Sending Request To This Action Method
        public async Task<IActionResult> Delete([FromRoute] int id, Employee employee) // Model Binding FromRoute To Prevent Overposting Attack
        {
            //if (ModelState.IsValid) // No Need Because No Input In View
            //{
            if (id != employee.Id)
            {
                return BadRequest("Invalid Id");
            }

            _unitOfWork.EmployeeRepository.Delete(employee);
            var count = await _unitOfWork.CompleteSaveChangesAsync();
            if (count > 0)
                {
                if (employee.ImageName is not null) // If User Upload New Image
                {
                    DocumentSetting.DeleteFile(employee.ImageName, "images"); // Call DeleteFile Method To Delete Old Image
                }
                return RedirectToAction("Index");
                }
           // }
            return View(employee);
        }
    }
}
