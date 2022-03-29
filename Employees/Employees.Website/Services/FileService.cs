using Employees.Website.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Employees.Website.Services
{
    public interface IFileService
    {
        List<Employee> GetEmployees(IFormFile file);
    }

    public class FileService : IFileService
    {
        private readonly IFormatService _formatService;

        public FileService(IFormatService formatService)
        {
            _formatService = formatService;
        }

        public List<Employee> GetEmployees(IFormFile file)
        {
            List<Employee> employees = new List<Employee>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine().Split(new char[] { ',' });

                    if (line.Any() == true)
                    {
                        employees.Add(new Employee
                        {
                            EmpID = int.Parse(line[0]),
                            ProjectID = int.Parse(line[1]),
                            DateFrom = _formatService.TryParse(line[2]),
                            DateTo = _formatService.TryParse(line[3])
                        });
                    }
                }
            }

            return employees;
        }
    }
}
