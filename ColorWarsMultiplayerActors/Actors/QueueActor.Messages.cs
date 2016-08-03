using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorWarsMultiplayerActors.Models;

namespace ColorWarsMultiplayerActors.Actors
{
    public partial class QueueActor
    {
        public class NewUserInQueue
        {
            public ClientData ClientData;

            public NewUserInQueue(ClientData clientData)
            {
                ClientData = clientData;
            }
        }

    }
}
