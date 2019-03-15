using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeServiceAPI.Models
{
    public class EmployeeData : IEmployeeData
    {
        public static List<Employee> _employees = new List<Employee>()
        {
            new Employee { Id = 1, FirstName = "siva1", LastName = "anand1", Gender = "Male", Salary = 1000 },
            new Employee { Id = 2, FirstName = "siva2", LastName = "anand2", Gender = "Male", Salary = 2000 },
            new Employee { Id = 3, FirstName = "siva3", LastName = "anand3", Gender = "Male", Salary = 3000 },
            new Employee { Id = 4, FirstName = "siva4", LastName = "anand4", Gender = "Male", Salary = 4000 }
        };
        public List<Employee> EmpValues()
        {
            return _employees;
        }
    }
}