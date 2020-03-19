using Owin;
using System.Web.Http;

namespace MartianRobots
{
    /// <summary>
    /// Startup class ues by Owin and Katina to create web api
    /// Starts with Katina automatically
    /// </summary>
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host.
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{idx}/{idy}",
                defaults: new
                {
                    idx = RouteParameter.Optional,
                    idy = RouteParameter.Optional
                }
            );

            app.UseWebApi(config);
        }
    }
}