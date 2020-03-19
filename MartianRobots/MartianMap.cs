using System;

namespace MartianRobots
{
    /// <summary>
    /// We assume mars is kind of flat square place with deathtraps on each side
    /// where X is death and O is safe
    /// And that robot can start at any place marked with safe O, scented with S.
    /// Assume robot landing position is at 1,1 point
    ///
    /// Supports square map of any reasonable size with o and x placements.
    /// </summary>
    public class MartianMap
    {
        //Allign with RobotController!!!
        public string deathCell = "x";
        public string safeCell = "o";
        public string scentCell = "s";
        private int[] mapMinMaxX = new int[] {20, 30};
        private int[] mapMinMaxY = new int[] {8, 13};
        private int deathCellPercent = 30;
        private string[,] map;
        private bool mapCreated = false;

        public string[,] GetMap()
        {
            if (!mapCreated)
            {
                Random rnd = new Random();
                map = new string[
                    rnd.Next(mapMinMaxX[0], mapMinMaxX[1]), 
                    rnd.Next(mapMinMaxY[0], mapMinMaxY[1])];
                CreateMap(map);
                mapCreated = true;
            }
            return map;
        }

        public string GetMapPosition(int x, int y)
        {
            return map[x, y];
        }

        public void MarkAsScent(int x, int y)
        {
            map[x, y] = scentCell;
        }

        public void MarkAsSafe(int x, int y)
        {
            map[x, y] = safeCell;
        }

        private void CreateMap(string[,] map)
        {
            Random rnd = new Random();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (rnd.Next(1, 100) > deathCellPercent) //Random dangers
                    {
                        map[i, j] = safeCell;
                    }
                    else
                    {
                        map[i, j] = deathCell;
                    }
                    
                }
            }
            //For autotests we need safe cell where we stand,
            //safe cell one north-forward and death cell two north forward
            map[rnd.Next(1, map.GetLength(0)),
                rnd.Next(1, map.GetLength(1))] = "W";
            map[1, 3] = deathCell;
            map[1, 2] = safeCell;
            map[1, 1] = safeCell;
        }
    }
}