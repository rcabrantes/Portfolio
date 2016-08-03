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

        public IActorRef ProxyActor;


        public LobbyActor(IActorRef proxyActor)
        {
            ProxyActor = proxyActor;
            ClientList = new Dictionary<string, ClientData>();
            Receive<ConnectMessage>(m=> {

                if(ClientList.Keys.Where(c=>c==m.ConnectionID).Any())
                {
                    ProxyActor.Tell(new ProxyActor.LobbyActorUserAlreadyConnected(ClientList[m.ConnectionID]));
                }
                else
                {
                    var userActor = Context.ActorOf<UserActor>();

                    var clientData = new ClientData(m.ConnectionID, m.UserName, userActor);
                    ClientList.Add(m.ConnectionID, clientData );

                    ProxyActor.Tell(new ProxyActor.LobbyActorStatus("Connected to lobby.", clientData));
                }
               

            });
        }
    }
}
