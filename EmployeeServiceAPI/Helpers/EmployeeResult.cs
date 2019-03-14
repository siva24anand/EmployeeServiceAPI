using EmployeeServiceAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EmployeeServiceAPI.Helpers
{
    public class EmployeeResult : IHttpActionResult
    {
        List<Employee> _employee;
        HttpRequestMessage _request;
        public EmployeeResult(List<Employee> employee, HttpRequestMessage request)
        {
            _employee = employee;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(_employee)),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }
    }
}