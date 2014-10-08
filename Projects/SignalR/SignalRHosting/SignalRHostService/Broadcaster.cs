using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalRHostService
{
    /// <summary>
    /// The broadcaster file for the SignalR clients
    /// </summary>
  public  class Broadcaster : Hub
    {
        public Task Broadcast(string message)
        {
            return Clients.All.Message(message);
        }
        public override Task OnConnected()
        {
            return Clients.All.Message(">> New client arrived");
        }
        public override Task OnDisconnected()
        {
            return Clients.All.Message(">> Client disconnected");
        }
    }
}
