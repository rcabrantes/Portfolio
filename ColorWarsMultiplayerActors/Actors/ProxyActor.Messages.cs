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

        public abstract class ClientCallCommand
        {
            public string ConnectionID { get; private set; }

            public ClientCallCommand(string connectionID)
            {
                ConnectionID = connectionID;
            }
        }


        public class GameInitCommand:ClientCallCommand
        {
            public GameGrid GameData { get; private set; }
            public GameInitCommand(string connectionID, GameGrid gameData):base(connectionID)
            {
                GameData = gameData;
            }
        }
        public abstract class StatusMessageBase
        {

            public string Message { get; private set; }
            public ClientData ClientData { get; private set; }

            public StatusMessageBase(string message, ClientData clientData)
            {
                Message = message;
                ClientData = clientData;
            }


        }

        public class ConnectedToLobbyStatus:StatusMessageBase
        {
            public ConnectedToLobbyStatus(ClientData clientData):base("Connected to lobby. Waiting for opponent.",clientData)
            {

            }
        }

        public class UserAlreadyConnectedStatus:StatusMessageBase
        {


            public UserAlreadyConnectedStatus( ClientData clientData):base("User already connected.",clientData)
            {
           }


        }

        public class OpponentFoundStatus : StatusMessageBase
        {
            public OpponentFoundStatus(ClientData clientData) : base("Opponent(s) found. Creating game.", clientData)
            { }
        }

        public class EnteredGameStatus:StatusMessageBase
        {
            public EnteredGameStatus(ClientData clientData):base("Entered game. Initializing.",clientData)
            { } 
        }
    }
}
