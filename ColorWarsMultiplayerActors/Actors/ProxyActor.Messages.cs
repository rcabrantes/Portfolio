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
        public class LobbyActorStatus
        {

            public string Message { get; private set; }
            public ClientData ClientData { get; private set; }

            public LobbyActorStatus(string message, ClientData clientData)
            {
                Message = message;
                ClientData = clientData;
            }


        }
    }
}
