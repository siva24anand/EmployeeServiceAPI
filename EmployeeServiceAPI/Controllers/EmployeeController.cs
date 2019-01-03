using EmployeeServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeServiceAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        static List<Employee> employees = new List<Employee>()
        {
            new Employee { ID = 1, FirstName = "siva1", LastName = "anand1", Gender = "Male", salary = 1000 },
            new Employee { ID = 2, FirstName = "siva2", LastName = "anand2", Gender = "Male", salary = 2000 },
            new Employee { ID = 3, FirstName = "siva3", LastName = "anand3", Gender = "Male", salary = 3000 },
            new Employee { ID = 4, FirstName = "siva4", LastName = "anand4", Gender = "Male", salary = 4000 }
        };
        
        // GET api/values
        public IEnumerable<Employee> Get()
        {
            return employees;
        }

        // GET api/values/5
        public Employee Get(int id)
        {
            return employees.Where(e => e.ID == id).FirstOrDefault();
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            try
            {
                employees.Add(employee);
                var message = Request.CreateResponse(HttpStatusCode.Created);
                message.Headers.Location = new Uri(Request.RequestUri + employees.Count.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Employee value)
        {
            employees.RemoveAt(id);
            employees.Add(value);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            employees.RemoveAt(id);
        }
    }
}
