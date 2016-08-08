using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWarsMultiplayerActors.Models
{
    public class GameGrid
    {
        public int HorizontalCount { get; private set; }
        public int VerticalCount { get; private set;}

        public GameCell[,] Grid { get; private set; }

        

        public GameGrid(int horizontalCount,int verticalCount)
        {
            HorizontalCount = horizontalCount;
            VerticalCount = verticalCount;

            Grid = new GameCell[horizontalCount, verticalCount];
            

            for(int x=0;x<horizontalCount;x++)
            {
                for(int y=0;y<verticalCount;y++)
                {
                    Grid[x, y] = new GameCell();
                }
            }


        }

        private void CaptureCell(int player, int x, int y)
        {
            Grid[x, y].Owner = player;
            CaptureNeighbors(player, x, y);

        }

        private void CaptureNeighbors(int player, int x, int y)
        {
            //Sets movements for each neighbor
            int[] dx = { -1, -1, 0, 0, 1, 1 };
            int[] dy;

            //Corrects y deltas according to column
            if ((x % 2) == 0)
            {
                dy = new int[] { 0, 1, -1, 1, 0, 1 };
            }
            else
            {
                dy = new int[] { -1, 0, -1, 1, -1, 0 };
            }

            //Checks each neighbor
            for (int i = 0; i < 6; i++)
            {
                var currentX = x + dx[i];
                var currentY = y + dy[i];



                //Verify if position is valid
                if (IsCellInsideGrid(currentX, currentY))
                {

                    var target = Grid[currentX, currentY];
                    var origin = Grid[x, y];

                    if (target.Color == origin.Color)
                    {
                        if (target.Owner == 0)
                        {
                            CaptureCell(player, currentX, currentY);
                        }
                    }

                }

            }
        }

        private bool IsCellInsideGrid(int X, int Y)
        {
            if(X < 0 || X > Grid.GetUpperBound(0))
            {
                return false;
            }
            if(Y < 0 || Y > Grid.GetUpperBound(1))
            {
                return false;
            }
            return true;
            
        }

        public void SetPlayerInitialCell(int player, int x, int y)
        {
            CaptureCell(player, x, y);


        }

        public void Play(int player, int newColor)
        {
            if(newColor > 0 && newColor < 9)
            {
                for (int x = 0; x <= Grid.GetUpperBound(0); x++)
                {
                    for(int y=0;y<=Grid.GetUpperBound(1);y++)
                    {
                        if (Grid[x,y].Owner == player)
                        {
                            Grid[x,y].Color = newColor;
                            CaptureNeighbors(player, x, y);
                        }
                    }
                }
            }
        }
    }
}
