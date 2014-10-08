using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MSExampleSignalR.Dto;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MSExampleSignalR
{
    public class MyHub : Hub
    {
        private static readonly Dictionary<string, UserDetail> UserList = new Dictionary<string, UserDetail>();
        private static readonly Dictionary<string, friendDetail> UserFriendsList = new Dictionary<string, friendDetail>();
        private static int userCount = 0;

        public void Broadcast()
        {
            Clients.All.broadcast(Context.ConnectionId);
        }

        public void AddMessage(string callerId, string friendId, string message)
        {
            Console.WriteLine("Hub AddMessage {0} {1}\n", friendId, message);

            Clients.Client(UserList[friendId].ContextId).addMessage(friendId, message);
        }

        public void AddFriend(string callerUserId, string friendUserId)
        {
            Console.WriteLine("Hub AddMessage {0} {1}\n", callerUserId, friendUserId);
            if (string.Equals(callerUserId,friendUserId))
            {
                Clients.Client(Context.ConnectionId).SendFriendAdded("Cannot add", friendUserId);
            }
            //Clients.All.addMessage(name, message);
            if (UserList.ContainsKey(callerUserId) && UserList.ContainsKey(friendUserId))
            {
                if (UserFriendsList.ContainsKey(callerUserId))
                {
                    UserFriendsList[callerUserId].FriendList.Add(
                        new Dictionary<string, string>
                        {
                            {friendUserId, UserList[friendUserId].ContextId}
                        });
                }
                else
                {
                    UserFriendsList.Add(callerUserId, new friendDetail());
                    UserFriendsList[callerUserId].FriendCount = UserFriendsList[callerUserId].FriendCount + 1;
                    UserFriendsList[callerUserId].FriendList = new List<Dictionary<string, string>>
                    {
                        new Dictionary<string, string>
                        {
                            {friendUserId, UserList[friendUserId].ContextId}
                        }
                    };
                }
            }
            string userIDOfCaller = String.Empty;
            string userIdOffriend = string.Empty;
            if (UserList.ContainsKey(callerUserId) && UserList.ContainsKey(friendUserId))
            {
                userIDOfCaller = UserList[callerUserId].ContextId;
                userIdOffriend = UserList[friendUserId].ContextId;
            }
            var clientList = new List<string>
            {
                userIDOfCaller,
                userIdOffriend
            };

            Console.WriteLine("friend online {0} \n", friendUserId);
            Clients.Client(Context.ConnectionId).SendFriendAdded(UserList[friendUserId].ContextId, friendUserId);
        }

        public Task Join(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public Task Leave(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }

        public void SendPrivateMessage(string msg)
        {
            Console.WriteLine(msg);
            Clients.All.addMessage(msg);
        }

        [HubMethodName("HeartBeatChange")]
        public Task Heartbeat()
        {
            Console.WriteLine("Hub Heartbeat\n");
            Clients.All.heartbeat();
            // return Clients.Group("Gm").heartbeat();
            return Clients.OthersInGroup("Gm").heartbeat();
        }

        public void SendHelloObject(HelloModel hello)
        {
            Console.WriteLine("Hub hello {0} {1}\n", hello.Molly, hello.Age);
            Clients.All.sendHelloObject(hello);
        }

        public override Task OnConnected()
        {
            Console.WriteLine("Hub OnConnected {0}  {1}\n", Context.ConnectionId, Context.RequestCookies["UserId"].Value);
            string userId = Context.RequestCookies["UserId"].Value;
            if (!UserList.ContainsKey(userId))
            {
                UserList.Add(userId,
                    new UserDetail
                    {
                        Count = userCount++,
                        ContextId = Context.ConnectionId,
                        LoginDateTime = DateTime.Now,
                    });
                Console.WriteLine(userId);
            }

            return (base.OnConnected());
        }

        public override Task OnDisconnected()
        {
            Console.WriteLine("Hub OnDisconnected {0}\n", Context.ConnectionId);
            string userId = Context.RequestCookies["UserId"].Value;
            if (UserList.ContainsKey(userId))
            {
                UserList.Remove(userId);
            }
            userCount--;
            return (base.OnDisconnected());
        }

        public override Task OnReconnected()
        {
            Console.WriteLine("Hub OnReconnected {0}\n", Context.ConnectionId);
            string userId = Context.RequestCookies["UserId"].Value;
            if (userId != null && !UserList.ContainsKey(userId))
            {
                UserList.Add(userId,
                    new UserDetail
                    {
                        Count = userCount++,
                        ContextId = Context.ConnectionId,
                        LoginDateTime = DateTime.Now,
                    });
            }
            return (base.OnDisconnected());
        }

        private class UserDetail
        {
            public string ContextId { get; set; }

            public DateTime LoginDateTime { get; set; }

            public int Count { get; set; }
        }

        private class friendDetail
        {
            public int FriendCount { get; set; }

            public List<Dictionary<string, string>> FriendList { get; set; }
        }
    }
}