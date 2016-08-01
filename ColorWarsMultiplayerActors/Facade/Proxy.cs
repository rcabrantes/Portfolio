using Akka.Actor;
using ColorWarsMultiplayerActors.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Facade
{
    public static class Proxy
    {
        private static IProxyClient _client;
        private static ActorSystem _system;

        private static IActorRef _lobbyActor;


        public static void ConnectClient(string connectionID,string userName)
        {
            if(_lobbyActor!=null)
            {
                _lobbyActor.Tell(new LobbyActor.ConnectMessage(connectionID,userName));
            }
        }

        public static void Subscribe(IProxyClient client)
        {
            _client = client;

            if(_system==null)
            {
                CreateSystem();
            }
        }

        private static void CreateSystem()
        {
            _system = ActorSystem.Create("ColorWarsSystem");
            _lobbyActor = _system.ActorOf<LobbyActor>("Lobby");


        }
    }
}
