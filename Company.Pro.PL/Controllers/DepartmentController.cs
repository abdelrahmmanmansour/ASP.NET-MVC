using AutoMapper;
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
        private readonly IMapper _mapper;

        // Ask CLR TO Create Object from DepartmentReository Class
        // Dependency Injection 
        public DepartmentController(IDepartmentRepository departmentReository,IMapper mapper)
        {
            _departmentReository = departmentReository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            // Get all departments from the database
            var departments = _departmentReository.GetAll();
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
                //var department = new Department()
                //{
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreateAt = model.CreateAt
                //};
                var department = _mapper.Map<Department>(model); // Using AutoMapper To Map From CreateDepartmentDto To Department
                var count = _departmentReository.Add(department);
                if (count > 0)
                {
                    TempData["Message"] = "Department Created Successfully"; // TempData To Send Message From Create Action To Index Action
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
            //var departmentDto = new CreateDepartmentDto()
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    CreateAt = department.CreateAt
            //};
            var departmentDto = _mapper.Map<CreateDepartmentDto>(department); // Using AutoMapper To Map From Department To CreateDepartmentDto
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
            //if (ModelState.IsValid) // No Need Because No Input In View
            //{
            if (id != department.Id)
                {
                    return BadRequest("Invalid Id");
                }
                var count = _departmentReository.Delete(department);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            //}
            return View(department);
        }
    }
}
