using Employees.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Employees.Website.Services
{
    public interface IEmployeeService
    {
        List<EmployeePair> GetEmployeePairs(List<Employee> employees);

        EmployeePair GetEmployeePairLongestPeriod(List<EmployeePair> employeePairs);
    }

    public class EmployeeService : IEmployeeService
    {
        public List<EmployeePair> GetEmployeePairs(List<Employee> employees)
        {
            var employeePairs = employees.Join(
                employees,
                emp2 => emp2.ProjectID,
                emp1 => emp1.ProjectID,
                (emp1, emp2) => new EmployeePair
                {
                    EmpID1 = emp1.EmpID,
                    EmpID2 = emp2.EmpID,
                    ProjectID = emp1.ProjectID,
                    DaysWorked = GetDaysWorked(emp1.DateFrom, emp1.DateTo, emp2.DateFrom, emp2.DateTo)
                }).Where(record => record.EmpID1 != record.EmpID2).ToList();

            for (int i = employeePairs.Count - 1; i >= 0; i--)
            {
                var duplicateEmployee = employeePairs.FirstOrDefault(record => record.EmpID1 == employeePairs[i].EmpID2 && record.EmpID2 == employeePairs[i].EmpID1 && record.ProjectID == employeePairs[i].ProjectID);

                if (duplicateEmployee != null)
                {
                    employeePairs.Remove(duplicateEmployee);
                }
            }

            return employeePairs;
        }

        public EmployeePair GetEmployeePairLongestPeriod(List<EmployeePair> employeePairs)
        {
            return employeePairs.GroupBy(x => new { x.EmpID1, x.EmpID2 }, y => new { y.DaysWorked })
                .Select((x, y) => new EmployeePair
                {
                    EmpID1 = x.Key.EmpID1,
                    EmpID2 = x.Key.EmpID2,
                    DaysWorked = x.Sum(f => f.DaysWorked)
                })
                .OrderByDescending(record => record.DaysWorked)
                .FirstOrDefault();
        }

        private int GetDaysWorked(DateTime dateFrom1, DateTime dateTo1, DateTime dateFrom2, DateTime dateTo2)
        {
            DateTime from = dateFrom1 >= dateFrom2 ? dateFrom1 : dateFrom2;
            DateTime to = dateTo1 <= dateTo2 ? dateTo1 : dateTo2;

            if (to < from)
            {
                return 0;
            }

            return (to - from).Days;
        }
    }
}
