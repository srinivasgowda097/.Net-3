using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SignalRHosting;

[assembly : OwinStartup(typeof(Startup))]
namespace SignalRHosting
{
   public class Startup
    {
       public void Configuration(IAppBuilder app)
       {
           app.UseCors(CorsOptions.AllowAll)
               //we activate CORS to provide support for browsers connecting to the service from other domains
               .MapSignalR<PingConnection>("/ping-connection")
               .MapSignalR("/hub", new HubConfiguration());
       }
    }
}
