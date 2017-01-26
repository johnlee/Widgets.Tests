using System.Web.Http;
using System.Web.Http.Routing;
using System.Net.Http;
using System.Web.Http.Hosting;

namespace Widgets.Tests.Mocks
{
    public static class MockHttpConfiguration
    {
        public static string mockUrl = "http://mockurl/api/mock";

        public static void HttpConfiguration(this ApiController controller)
        {
            HttpConfiguration mockConfiguration = new HttpConfiguration();

            var route = mockConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var routeData = new HttpRouteData(route,
                new HttpRouteValueDictionary
                {
                    { "id", "1" },
                    { "controller", "Users" }
                }
            );

            controller.Configuration = mockConfiguration;
        }

        public static void HttpPost(this ApiController controller)
        {
            HttpRequestMessage mockHttpRequest = new HttpRequestMessage(HttpMethod.Post, mockUrl);
            controller.Request = mockHttpRequest;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, controller.Configuration);
        }

        public static void HttpGet(this ApiController controller)
        {
            HttpRequestMessage mockHttpRequest = new HttpRequestMessage(HttpMethod.Get, mockUrl);
            controller.Request = mockHttpRequest;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, controller.Configuration);
        }

        public static void HttpPut(this ApiController controller)
        {
            HttpRequestMessage mockHttpRequest = new HttpRequestMessage(HttpMethod.Put, mockUrl);
            controller.Request = mockHttpRequest;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, controller.Configuration);
        }

        public static void HttpDelete(this ApiController controller)
        {
            HttpRequestMessage mockHttpRequest = new HttpRequestMessage(HttpMethod.Delete, mockUrl);
            controller.Request = mockHttpRequest;
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, controller.Configuration);
        }
    }
}
