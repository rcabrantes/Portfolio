using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Portfolio
{
    public class ColorWarsHub : Hub
    {

        public void ask()
        {

        }

        public void connect(string username)
        {
            Actors.Proxy.Connect(username,Context.ConnectionId);
        }
    }
}