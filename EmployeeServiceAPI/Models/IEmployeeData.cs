using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServiceAPI.Models
{
    public interface IEmployeeData
    {
        List<Employee> EmpValues();
    }
}
