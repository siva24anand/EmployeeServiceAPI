using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EmployeeServiceAPI.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        [ActionName("GetData")]
        [System.Web.Mvc.OutputCache(Duration =10)]
        public string GetData()
        {
            return DateTime.Now.ToString();
        }

        [HttpGet]
        public async Task<string> GetDataAsync()
        {
            var watch = new Stopwatch();
            watch.Start();

            var localdatatask =   GetDataLocal();
            var localdatatask1 = GetDataLocal1();
            var localdatatask2 = GetDataLocal2();

            var localdata = await localdatatask;
            var localdata1 = await localdatatask1;
            var localdata2 = await localdatatask2;

            watch.Stop();
            var local = "GetDataAsync: " + localdata + localdata1 + localdata2 + " Timer: " + watch.ElapsedMilliseconds;
            return local;
        }

        [HttpGet]
        public string GetDataSync()
        {
            var watch = new Stopwatch();
            watch.Start();

            var localdata = Task.Run(GetDataLocal).Result;
            var localdata1 = Task.Run(GetDataLocal1).Result;
            var localdata2 = Task.Run(GetDataLocal2).Result;
            
            watch.Stop();
            var local = "GetDataSync: " + localdata + localdata1 + localdata2 + " Timer: " + watch.ElapsedMilliseconds;
            return local;
        }

        private async Task<string> GetDataLocal()
        {
            await Task.Delay(2000);
            return "GetDataLocal; ";
        }

        private async Task<string> GetDataLocal1()
        {
            await Task.Delay(4000);
            return "GetDataLocal1; ";
        }
        private async Task<string> GetDataLocal2()
        {
            await Task.Delay(1000);
            return "GetDataLocal2; ";
        }

    }
}