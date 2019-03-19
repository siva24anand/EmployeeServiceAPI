using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace EmployeeServiceAPI.Filters
{
    public class CustomException:ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            var exceptionMessage = string.Empty;
            if (actionExecutedContext.Exception.InnerException == null)
                exceptionMessage = actionExecutedContext.Exception.Message;
            else
                exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
            Trace.WriteLine("From CustomException in " + actionExecutedContext.ActionContext.ActionDescriptor.ActionName + " " + exceptionMessage);
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("Unexpected error occured, Please contact Admin")
                //ReasonPhrase = "Unexpected error occured, Please contact Admin"
            };
            actionExecutedContext.Response = response;
        }
    }
}