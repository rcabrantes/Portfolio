using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Actors
{
    public partial class UserActor
    {
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
