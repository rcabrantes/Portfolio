using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Models
{
    public class ClientData
    {
        public string ConnectionID { get; private set; }
        public string UserName { get; set; }
        public IActorRef UserActor { get; private set; }

        public ClientData(string connectionID,string userName, IActorRef userActor)
        {
            ConnectionID = connectionID;
            UserName = userName;
            UserActor = userActor;
        }
    }
}
