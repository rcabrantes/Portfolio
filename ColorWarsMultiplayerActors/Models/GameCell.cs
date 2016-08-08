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


        public GameCell()
        {
            Owner = 0;
            Color = new Random().Next(9);
        }
    }
}
