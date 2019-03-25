using EmployeeServiceAPI;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace APIHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup1>("http://localhost:17498"))
            {
                Console.WriteLine("Server started. Press any key to exit..");
                Console.ReadLine();
            }
        }
    }
}
