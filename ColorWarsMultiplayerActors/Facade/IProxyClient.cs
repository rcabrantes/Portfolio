using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorWarsMultiplayerActors.Models;

namespace ColorWarsMultiplayerActors.Facade
{
    public interface IProxyClient
    {

        void SystemStatusLog(string connectionID, string message);
        void GameInit(string connectionID,int playerNumber,GameCell[,] grid);
    }
}
