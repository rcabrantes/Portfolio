using Akka.Actor;
using ColorWarsMultiplayerActors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Actors
{
    public partial class UserActor : ReceiveActor
    {
        public IActorRef mProxyActor;
        public ClientData MyClient;
        public IActorRef GameActor;

        public UserActor(string connectionID, string userName, IActorRef proxyActor)
        {
            MyClient = new ClientData(connectionID,userName,Self);
            mProxyActor = proxyActor;

            Receive<WelcomeToGameMessage>(m => {
                mProxyActor.Tell(new ProxyActor.EnteredGameStatus(MyClient));

                GameActor = m.GameActor;
            });
        }
    }
}
