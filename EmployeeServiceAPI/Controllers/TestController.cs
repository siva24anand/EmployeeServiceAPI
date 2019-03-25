using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EmployeeServiceAPI.Controllers
{
    public class TestController : ApiController
    {
        [Authorize]
        [HttpGet]
        [ActionName("GetData")]
        [System.Web.Mvc.OutputCache(Duration =10)]
        public string GetData()
        {
            return DateTime.Now.ToString();
        }

        [HttpGet]
        public string GetAuthData()
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

        [HttpGet]
        public HttpResponseMessage DownloadFile()
        {
            var filePath = HttpContext.Current.Server.MapPath("~/DataFiles/70-487 Topics.xlsx");
            var fileInfo = new FileInfo(filePath);

            var response = Request.CreateResponse();
            response.Headers.AcceptRanges.Add("bytes");
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StreamContent(fileInfo.OpenRead());
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = filePath;
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //response.Content.Headers.ContentLength = fileInfo.Length;
            return response;
        }

        [HttpGet]
        public HttpResponseMessage GetVideo()
        {
            var fileName = "EI_KT.mp4";
            var response = Request.CreateResponse();
            response.Content = new PushStreamContent((Action<Stream, HttpContent, TransportContext>)OnStreamAvailable);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attchment");
            response.Content.Headers.ContentDisposition.FileName = fileName;
            return response;
        }

        private async void OnStreamAvailable(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                var fileName = @"D:\MyProjects\WorkingFiles\EI_KT.mp4";
                var buffer = new byte[65536];
                using (var video = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    var length = (int)video.Length;
                    var bytesReads = 1;
                    while(length > 0 && bytesReads >0)
                    {
                        bytesReads = video.Read(buffer, 0, Math.Min(length, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, bytesReads);
                        length -= bytesReads;
                    }
                }
            }
            catch(HttpException ex)
            {
                return;
            }
            finally
            {
                outputStream.Close();
            }
        }

    }
}