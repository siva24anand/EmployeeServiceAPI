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
            new Employee { Id = 1, FirstName = "siva1", LastName = "anand1", Gender = "Male", Salary = 1000 },
            new Employee { Id = 2, FirstName = "siva2", LastName = "anand2", Gender = "Male", Salary = 2000 },
            new Employee { Id = 3, FirstName = "siva3", LastName = "anand3", Gender = "Male", Salary = 3000 },
            new Employee { Id = 4, FirstName = "siva4", LastName = "anand4", Gender = "Male", Salary = 4000 }
        };
        
        // GET api/values
        public IHttpActionResult Get()
        {
            //return employees;
            if(employees != null && employees.Count >0)
            {
                var response = Request.CreateResponse(employees);
                //return response;
                return Ok(employees);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/values/5
        [HttpGet]
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetEmployee(int id)
        {
            if(employees.Any(e=>e.Id == id))
            {
                var response = Request.CreateResponse<Employee>(employees.Where(e => e.Id == id).FirstOrDefault());
                return response;
            }   
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }   
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            try
            {
                if(employee != null)
                {
                    employees.Add(employee);
                    var message = Request.CreateResponse(HttpStatusCode.Created);
                    message.Headers.Location = new Uri(Request.RequestUri + employees.Count.ToString());
                    return message;
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Empty input data");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody]Employee value)
        {
            if(employees.Any(e=>e.Id == id))
            {
                employees.Where(e => e.Id == id).Select(s => {
                    s.FirstName = value.FirstName;
                    s.Gender = value.Gender;
                    s.LastName = value.LastName;
                    s.Salary = value.Salary;
                    return s;
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                employees.Add(value);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            if(employees.Any(e=>e.Id == id))
            {
                employees.Remove(employees.Find(e => e.Id == id));
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Patch(int id, [FromBody]string lastName)
        {
            if(employees.Any(e=>e.Id == id))
            {
                var employee = employees.FirstOrDefault(e => e.Id == id);
                employee.LastName = lastName;
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [NonAction]
        public string AddEmployee(Employee employee)
        {
            employees.Add(employee);
            return employees.Count.ToString();
        }
    }
}
