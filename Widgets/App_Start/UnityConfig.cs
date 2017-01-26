using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using Widgets.Data;

namespace Widgets
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Injecting the Repository from Widgets.Data
            container.RegisterType<IRepository, Repository>(); // Using the MockRepository since we dont have a real database

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}