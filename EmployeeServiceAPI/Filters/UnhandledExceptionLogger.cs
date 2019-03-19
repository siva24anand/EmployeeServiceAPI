using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace EmployeeServiceAPI.Filters
{
    public class UnhandledExceptionLogger: ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            base.Log(context);
            var log = context.Exception.Message;
            Trace.WriteLine("From Exception Logger: " +log);
        }
    }
}