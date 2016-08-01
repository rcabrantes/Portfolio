using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Models
{
    public class PingMessage
    {
        public DateTime TimeStamp { get; private set; }

        public PingMessage(DateTime stamp)
        {
            TimeStamp = stamp;
        }

    }
    public class PongMessage
    {
        public DateTime TimeStamp { get; private set; }

        public PongMessage(DateTime stamp)
        {
            TimeStamp = stamp;
        }

    }

}
