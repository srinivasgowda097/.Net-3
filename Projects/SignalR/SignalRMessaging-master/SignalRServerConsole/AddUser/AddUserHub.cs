using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddUser.Data;
using ExampleSignalR.DAL;
using Microsoft.AspNet.SignalR;

namespace AddUser
{
    public class AddUserHub : Hub
    {
        public void Broadcast()
        {
            Clients.Caller.broadcast(Context.ConnectionId);
        }
        public void AddUser(User userDetail)
        {
           Console.WriteLine("Hub OnConnected {0}\n", Context.ConnectionId);
           var isSucceed =  new Command().AddUser(userDetail.UserName, userDetail.Password, userDetail.FirstName,userDetail.LastName);
            Clients.Caller.addMessage(isSucceed);
        }
        public override Task OnConnected()
        {
            Console.WriteLine("Hub OnConnected {0}\n", Context.ConnectionId);
            Broadcast();
            return (base.OnConnected());
        }

        public override Task OnDisconnected()
        {
            Console.WriteLine("Hub OnDisconnected {0}\n", Context.ConnectionId);
            return (base.OnDisconnected());
        }

        public override Task OnReconnected()
        {
            Console.WriteLine("Hub OnReconnected {0}\n", Context.ConnectionId);
            return (base.OnDisconnected());
        }
    }
}
