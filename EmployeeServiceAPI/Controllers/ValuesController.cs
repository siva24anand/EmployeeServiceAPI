using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeServiceAPI.Controllers
{
    public class ValuesController : ApiController
    {
        static List<string> strings = new List<string>()
        {
            "value0", "value1", "value2"
        };
        // GET api/values
        public IEnumerable<string> Get()
        {
            return strings;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return strings[id];
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]string value)
        {
            try
            {
                strings.Add(value);
                var message = Request.CreateResponse(HttpStatusCode.Created);
                message.Headers.Location = new Uri(Request.RequestUri + strings.Count.ToString());
                return message;
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromUri]string value)
        {
            strings[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            strings.RemoveAt(id);
        }
    }
}
