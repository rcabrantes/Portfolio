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

        public IActorRef mProxyActor;
        public IActorRef mQueueActor;


        public LobbyActor(IActorRef proxyActor,IActorRef queueActor)
        {
            mQueueActor = queueActor;
            mProxyActor = proxyActor;
            ClientList = new Dictionary<string, ClientData>();
            Receive<ConnectMessage>(m=> {

                if(ClientList.Keys.Where(c=>c==m.ConnectionID).Any())
                {
                    mProxyActor.Tell(new ProxyActor.UserAlreadyConnectedStatus(ClientList[m.ConnectionID]));
                }
                else
                {
                    var userActor = Context.ActorOf(Props.Create(() => new UserActor(m.ConnectionID,m.UserName,mProxyActor)));

                    var clientData = new ClientData(m.ConnectionID, m.UserName, userActor);


                    ClientList.Add(m.ConnectionID, clientData );


                    mQueueActor.Tell(new QueueActor.NewUserInQueue(clientData));
                    mProxyActor.Tell(new ProxyActor.ConnectedToLobbyStatus(clientData));
                }
               

            });
        }
    }
}
