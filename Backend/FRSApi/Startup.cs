using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Web.Http;

namespace FRSApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "Default",
                "{controller}/{id}/{date}",
                new
                {
                    id = RouteParameter.Optional,
                    date = RouteParameter.Optional
                });

            app.UseWebApi(config);
        }
    }
}
