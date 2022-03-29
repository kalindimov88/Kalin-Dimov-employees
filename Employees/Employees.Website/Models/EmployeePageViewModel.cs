using System.Collections.Generic;

namespace Employees.Website.Models
{
    public class EmployeePageViewModel
    {
        public List<EmployeePair> EmployeePairs { get; set; }

        public EmployeePair EmployeePairLongestPeriod { get; set; }
    }
}
