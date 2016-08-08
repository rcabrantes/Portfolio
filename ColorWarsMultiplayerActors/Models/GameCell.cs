using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Models
{
    public class GameCell
    {
        public int Owner { get; set; }

        public int Color { get; set; }

        public static Random myRandom;

        public GameCell()
        {

            if(myRandom==null)
            {
                myRandom = new Random();
            }

            Owner = 0;
            Color = myRandom.Next(9);
        }
    }
}
