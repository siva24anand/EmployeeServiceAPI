using EmployeeServiceAPI.Helpers;
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
        private static List<Employee> _employees;
        private IEmployeeData _employeeData;
        public EmployeeController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
            _employees = _employeeData.EmpValues();
        }

        [HttpGet]
        [ActionName("GetEmployee")]
        public HttpResponseMessage GetEmployee()
        {
            //return employees;
            if (_employees != null && _employees.Count > 0)
            {
                var response = Request.CreateResponse(_employees);
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // GET api/values
        [HttpGet]
        [ActionName("GetEmployeeResult")]
        public IHttpActionResult GetEmployeeResult()
        {
            //return employees;
            if(_employees != null && _employees.Count >0)
            {
                return new EmployeeResult(_employees, Request);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/values/5
        [HttpGet]
        [ActionName("GetEmployeeById")]
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetEmployee(int id)
        {
            if(_employees.Any(e=>e.Id == id))
            {
                var response = Request.CreateResponse<Employee>(_employees.Where(e => e.Id == id).FirstOrDefault());
                return response;
            }   
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }   
        }

        // POST api/values
        [HttpPost]
        [ActionName("PostEmployee")]
        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            try
            {
                if(employee != null)
                {
                    _employees.Add(employee);
                    var message = Request.CreateResponse(HttpStatusCode.Created);
                    message.Headers.Location = new Uri(Request.RequestUri + _employees.Count.ToString());
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
        [HttpPut]
        [ActionName("PutEmployee")]
        public HttpResponseMessage Put(int id, [FromBody]Employee value)
        {
            if(_employees.Any(e=>e.Id == id))
            {
                _employees.Where(e => e.Id == id).Select(s => {
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
                _employees.Add(value);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        [ActionName("DeleteEmployee")]
        public HttpResponseMessage Delete(int id)
        {
            if(_employees.Any(e=>e.Id == id))
            {
                _employees.Remove(_employees.Find(e => e.Id == id));
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        [HttpPatch]
        [ActionName("PatchEmployee")]
        public HttpResponseMessage Patch(int id, [FromBody]string lastName)
        {
            if(_employees.Any(e=>e.Id == id))
            {
                var employee = _employees.FirstOrDefault(e => e.Id == id);
                employee.LastName = lastName;
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [NonAction]
        public string AddEmployee(Employee employee)
        {
            _employees.Add(employee);
            return _employees.Count.ToString();
        }
    }
}
