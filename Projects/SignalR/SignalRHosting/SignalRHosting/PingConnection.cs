using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRHosting
{
    public class PingConnection : PersistentConnection
    {
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            if (data == "Ping")
            {
                Console.WriteLine("[Connection] Ping received");
                return Connection.Send(
                    connectionId,
                    "Ping received at " + DateTime.Now.ToLongTimeString()
                    );
            }

            return base.OnReceived(request, connectionId, data);
        }
    }
}