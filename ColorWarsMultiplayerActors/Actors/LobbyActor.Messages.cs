using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Actors
{
    public partial class LobbyActor
    {

        public class ConnectMessage
        {
            public string ConnectionID { get; private set; }
            public string UserName { get; private set; }

            public ConnectMessage(string connectionID,string userName)
            {
                this.ConnectionID = connectionID;
                this.UserName = userName;
            }
        }
    }
}
