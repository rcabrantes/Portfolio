using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorWarsMultiplayerActors.Models;

namespace ColorWarsMultiplayerActors.Actors
{
    public partial class GameActor : ReceiveActor
    {
        public IActorRef mProxyActor;
        public List<ClientData> Players;
        public GameGrid GameData;

        public const int GAME_HORIZONTAL_SIZE = 72;

        public const int GAME_VERTICAL_SIZE = 36;

        public GameActor(IActorRef proxyActor)
        {
            mProxyActor = proxyActor;

            Receive<NewGameMessage>(m=> {
                Players = m.Players;
                
                TellEveryPlayer(new UserActor.WelcomeToGameMessage(Self));
                
                GameData = new GameGrid(GAME_HORIZONTAL_SIZE, GAME_VERTICAL_SIZE);
                TellEveryPlayer(new UserActor.GameInitializedMessage(GameData));
                

            });


        }

        private void TellEveryPlayer(object message)
        {
            foreach (var player in Players)
            {
                player.UserActor.Tell(message);
            }
        }

        
        

    }
}
