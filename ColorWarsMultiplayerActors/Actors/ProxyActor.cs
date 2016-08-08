using Akka.Actor;
using ColorWarsMultiplayerActors.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Actors
{
    public partial class ProxyActor:ReceiveActor
    {
        public ProxyActor()
        {
            Receive<StatusMessageBase>(m=> {
                Proxy.ServerStatusLog(m.ClientData.ConnectionID, m.Message);
            });

            Receive<GameInitCommand>(m => {

            });
        }
    }
}
