using EmployeeServiceAPI.Models;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace EmployeeServiceAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterSingleton<IEmployeeData, EmployeeData>();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}