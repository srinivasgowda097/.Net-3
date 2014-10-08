using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MSExampleSignalR;
using NUnit.Framework;
using Rhino.Mocks;

namespace MSExampleSignalRTest
{
    [TestFixture]
    public class MyHubTest
    {
        [Test]
        public void On_Connected_sends_a_broadcast_with_a_specific_message()
        {
            // 1) ARRANGE
            var hub = new MyHub();
            var connId = "1234";
            var host = "myhost";
            var repositary = new MockRepository();
            var expectedMessage = "New connection " + connId + " at " + host;
            // 1.a) Set up headers
            var mockRequest = repositary.Stub<IRequest>();
            hub.Context = repositary.Stub<HubCallerContext>(mockRequest, connId);
            var cookies = repositary.Stub<IDictionary<string, Cookie>>();
            cookies["UserId"] = new Cookie("a","a");
            hub.Context.Request.Expect(m => m.Cookies).Return(cookies);
           // hub.Context.RequestCookies.Expect(m => m["UserId"]).Return(new Cookie("a", "a"));
           // hub.Context.Request.Cookies.Expect(m => m["UserId"]).Return(new Cookie("a", "a"));
                // 1.c) Set up capture of client method call
            var clientMethodInvoked = false;
            var messageSent = "";
            dynamic all = new ExpandoObject();
            all.Message = new Func<string, Task>((string message) =>
            {
                clientMethodInvoked = true;
                messageSent = message;
                return Task.FromResult(true);
            });
            var mockClients = repositary.Stub<IHubCallerConnectionContext>();
            hub.Clients = mockClients;
            
            // 2) ACT
            hub.OnConnected().Wait();
            // 3) ASSERT
            Assert.IsTrue(clientMethodInvoked, "No client methods invoked.");
            Assert.AreEqual(expectedMessage, messageSent);
        }
    }
}
