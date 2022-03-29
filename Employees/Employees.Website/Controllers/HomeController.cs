using Employees.Website.Services;
using Employees.Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IFileService _fileService;

        public HomeController(IEmployeeService employeeService, IFileService fileService)
        {
            _employeeService = employeeService;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            return View(new EmployeePageViewModel());
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            if (file == null)
            {
                ViewData["Error"] = "You need to select a file.";

                return View(new EmployeePageViewModel());
            }

            var employees = _fileService.GetEmployees(file);
            var employeePairs = _employeeService.GetEmployeePairs(employees);
            var employeePairLongestPeriod = _employeeService.GetEmployeePairLongestPeriod(employeePairs);

            return View(new EmployeePageViewModel { EmployeePairs = employeePairs, EmployeePairLongestPeriod = employeePairLongestPeriod });
        }
    }
}
