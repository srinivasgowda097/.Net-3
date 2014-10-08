using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ExampleSignalR.DAL;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

namespace MSExampleSignalR
{
    public class CustomLoginMiddleware : OwinMiddleware
    {
        
        public CustomLoginMiddleware(OwinMiddleware next)
            : base(next)
        {
        }
        public override async Task Invoke(IOwinContext context)
        {
            var path = context.Request.Path.Value.ToLower();

            if (context.Request.Method == "POST" && path.EndsWith("/account/remotelogin"))
            {
                var form = await context.Request.ReadFormAsync();
                var userName = form["username"];
                var password = form["password"];
                string name = string.Empty;
                if ((name = validate(userName, password)) != string.Empty)
                {
                    var identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationType
                    );
                    identity.AddClaim(
                    new Claim(ClaimTypes.Name, userName)
                    );
                    identity.AddClaim(
                    new Claim(ClaimTypes.Role, "user")
                    );
                    context.Authentication.SignIn(identity);
                    context.Response.StatusCode = 200;
                    context.Response.Cookies.Append("userName",name);
                   context.Response.Cookies.Append("userId",userName);
                    context.Response.ReasonPhrase = "Authorized";
                }
                else
                {
                    context.Response.StatusCode = 401;
                    context.Response.ReasonPhrase = "Unauthorized";
                }
                return;
            }
            await Next.Invoke(context);
           

        }

        private string validate(string username, string password)
        {
          //  return true;
            return new Command().GetUser(username, password);
        }
    }
}
