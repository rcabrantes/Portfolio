using ColorWarsMultiplayerActors.Facade;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActorSideProxy = ColorWarsMultiplayerActors.Facade.Proxy;

namespace Portfolio
{
    public static class Actors
    {
        public static WebActorProxy Proxy;

        public static void Init()
        {
            Proxy = new WebActorProxy();
            ActorSideProxy.Subscribe(Proxy);
        }
    }



    public class WebActorProxy:IProxyClient
    {


#region "Website side calls"

        public void Connect(string userName,string connectionID)
        {
            ActorSideProxy.ConnectClient(connectionID, userName);
        }


        

#endregion

#region "Actors side calls"
        public void SystemStatusLog(string connectionID,string message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ColorWarsHub>();
            hubContext.Clients.Client(connectionID).serverStatusLog(message);
        }

#endregion
    }
}