using Akka.Actor;
using ColorWarsMultiplayerActors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Actors
{
    public partial class UserActor
    {
        public class GameInitializedMessage
        {
            public GameGrid GameData { get; private set; }

            public GameInitializedMessage(GameGrid grid)
            {
                GameData = grid;
            }
        }

        public class WelcomeToGameMessage
        {
            public IActorRef GameActor { get; private set; }

            public WelcomeToGameMessage(IActorRef gameActor)
            {
                GameActor = gameActor;
            }
        }
    }
}
