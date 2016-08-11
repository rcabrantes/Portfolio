using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Models
{
    public class PlayerData
    {
        public ClientData ClientData { get; private set; }
        public int PlayerNumber { get; private set; }


        public PlayerData(string connectionID, string userName, IActorRef userActor,int playerNumber)
        {
            ClientData = new ClientData(connectionID, userName, userActor);
            PlayerNumber = playerNumber;
        }

        public PlayerData(ClientData clientData,int playerNumber)
        {
            PlayerNumber = playerNumber;
            ClientData = clientData;
        }


    }
}
