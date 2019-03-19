using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace EmployeeServiceAPI.Filters
{
    public class GlobalExceptionHandler:ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("From Exception Handler: Please contact Admin")
            };
            context.Result = new ErrorMessageResult(response);
        }
    }
    public class ErrorMessageResult : IHttpActionResult
    {
        private HttpResponseMessage _httpResponseMessage;
        public ErrorMessageResult(HttpResponseMessage httpResponseMessage)
        {
            _httpResponseMessage = httpResponseMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_httpResponseMessage);
        }
    }
}