using ColorWarsMultiplayerActors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Actors
{
    public partial class ProxyActor
    {


        public abstract class LobbyActorStatusMessage
        {

            public string Message { get; private set; }
            public ClientData ClientData { get; private set; }

            public LobbyActorStatusMessage(string message, ClientData clientData)
            {
                Message = message;
                ClientData = clientData;
            }


        }

        public class ConnectedToLobbyStatus:LobbyActorStatusMessage
        {
            public ConnectedToLobbyStatus(ClientData clientData):base("Connected to lobby. Waiting for opponent.",clientData)
            {

            }
        }

        public class UserAlreadyConnectedStatus:LobbyActorStatusMessage
        {


            public UserAlreadyConnectedStatus( ClientData clientData):base("User already connected.",clientData)
            {
           }


        }
    }
}
