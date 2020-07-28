using EmployeeManagementDemo.Models;
using EmployeeManagementDemo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeesRepo;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;
        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment,
            ILogger<HomeController> logger)
        {
            _employeesRepo = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }
        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = _employeesRepo.GetAllEmployees();
            return View(model);
        }
        public ViewResult Details(int? id)
        {
            _logger.LogTrace("Trace method");
            _logger.LogDebug("Debug method");
            _logger.LogInformation("Information method");
            _logger.LogWarning("Warning method");
            _logger.LogError("Error method");
            _logger.LogCritical("Critical method");
            //throw new Exception("Unhandled exception");
            Employee employee = _employeesRepo.GetEmployee(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetailsViewModel viewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };
            return View(viewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    uniqueFileName = ProcessUploadFile(model);
                }

                Employee newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeesRepo.Add(newEmployee);
                return RedirectToAction("Details", new { id = newEmployee.ID });
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = _employeesRepo.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                Id = employee.ID,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeesRepo.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadFile(model);
                }
                _employeesRepo.Update(employee);
                return RedirectToAction("index");       
            }
            return View();
        }
        private string ProcessUploadFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = "";
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.Photo.CopyTo(fileStream);
            }            
            return uniqueFileName;
        }
    }
}
