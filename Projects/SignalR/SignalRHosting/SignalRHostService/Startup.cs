using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SignalRHostService;

[assembly: OwinStartup(typeof(Startup))]
namespace SignalRHostService
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll)
            .MapSignalR();
        }
    }
}
