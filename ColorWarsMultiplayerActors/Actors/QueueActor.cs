using Akka.Actor;
using ColorWarsMultiplayerActors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Actors
{
    public partial class QueueActor:ReceiveActor
    {
        public IActorRef ProxyActor;
        public Dictionary<string, ClientData> UserQueue;


        public QueueActor(IActorRef proxyActor)
        {
            UserQueue = new Dictionary<string, ClientData>();
            ProxyActor = proxyActor;

            Receive<NewUserInQueue>(m=> {
                //Do nothing if user is already in
                if (!UserQueue.Keys.Where(c => c == m.ClientData.ConnectionID).Any())
                {
                    UserQueue.Add(m.ClientData.ConnectionID, m.ClientData);

                }


            });

        }
    }
}
