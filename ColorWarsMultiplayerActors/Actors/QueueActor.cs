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
        private const int PLAYERS_PER_GAME = 2;


        public QueueActor(IActorRef proxyActor)
        {
            UserQueue = new Dictionary<string, ClientData>();
            ProxyActor = proxyActor;

            Receive<NewUserInQueue>(m=> {
                //Do nothing if user is already in
                if (!UserQueue.Keys.Where(c => c == m.ClientData.ConnectionID).Any())
                {
                    UserQueue.Add(m.ClientData.ConnectionID, m.ClientData);

                    Self.Tell(new CheckQueueStatus());
                }


            });

            Receive<CheckQueueStatus>(m=> {
                if(UserQueue.Count>=PLAYERS_PER_GAME)
                {
                    var gamePlayers = UserQueue.Values.Take(PLAYERS_PER_GAME).ToList();

                    for (int i = 0; i < PLAYERS_PER_GAME; i++)
                    {
                        UserQueue.Remove(gamePlayers[i].ConnectionID);
                        ProxyActor.Tell(new ProxyActor.OpponentFoundStatus(gamePlayers[i]));

                        
                    }

                    var gameActor = Context.ActorOf(Props.Create(() => new GameActor(ProxyActor)));


                    gameActor.Tell(new GameActor.NewGameMessage(gamePlayers));

                }
            });

        }
    }
}
