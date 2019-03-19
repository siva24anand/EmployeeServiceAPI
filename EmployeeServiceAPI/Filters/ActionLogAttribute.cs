using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EmployeeServiceAPI.Filters
{
    public class ActionLogAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            Trace.WriteLine("From LogAttribute: Before executing action method - " + actionContext.ActionDescriptor.ActionName);
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            Trace.WriteLine("From LogAttribute: After executing action method - " + actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
        }
    }
}