using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalRHosting
{
   public class PingHub : Hub
    {
       public Task Ping()
       {
           Console.WriteLine("[Hub] Ping received");
           return Clients.Caller.Message(
           "Ping received at " + DateTime.Now.ToLongTimeString());
       }
    }
}
