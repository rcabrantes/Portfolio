using Akka.Actor;
using ColorWarsMultiplayerActors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Actors
{
    public partial class LobbyActor : ReceiveActor
    {
        public Dictionary<string,ClientData> ClientList;


        public LobbyActor()
        {
            ClientList = new Dictionary<string, ClientData>();
            Receive<ConnectMessage>(m=> {
                ClientList.Add(m.ConnectionID, new ClientData(m.ConnectionID,m.UserName,null));
            });
        }
    }
}
