using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MartianRobotsUI
{
    class Startup
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
