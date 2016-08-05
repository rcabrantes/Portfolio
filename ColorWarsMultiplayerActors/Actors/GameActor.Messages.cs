using ColorWarsMultiplayerActors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Actors
{
    public partial class GameActor
    {


        public class NewGameMessage
        {
            public List<ClientData> Players;

            public NewGameMessage(List<ClientData> gamePlayers)
            {
                Players = gamePlayers;
            }
        }
    }
}
