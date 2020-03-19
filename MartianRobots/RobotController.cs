using SharedFiles;
using System;

namespace MartianRobots
{
    /// <summary>
    /// Robot controller. Allows to it to move, die and respawn at 1,1
    /// </summary>
    public class RobotController
    {
        private GetResponse.Directions currentDirection;
        private bool currentScent = false;
        private int currentX = 1;
        private int currentY = 1;
        private string deathCell = "x";
        private string safeCell = "o";
        private string scentCell = "s";

        public GetResponse ProcessRobotInput(string inputs, MartianMap map)
        {
            var response = new GetResponse();

            foreach (var input in inputs.ToUpperInvariant())
            {
                if (input == nameof(GetResponse.Turns.L)[0])
                {
                    if ((int)currentDirection > 0)
                    {
                        currentDirection = currentDirection - 1;
                    }
                    else
                    {
                        currentDirection = GetResponse.Directions.West;
                    }
                }

                if (input == nameof(GetResponse.Turns.R)[0])
                {
                    if ((int)currentDirection < 3)
                    {
                        currentDirection = currentDirection + 1;
                    }
                    else
                    {
                        currentDirection = GetResponse.Directions.North;
                    }
                }

                if (input == nameof(GetResponse.Turns.F)[0])
                {
                    var previousCell = new int[]
                    {
                        currentX, currentY
                    };

                    switch (currentDirection)
                    {
                        case GetResponse.Directions.North:
                            if (!HasScent(currentX, currentY, map))
                            {
                                currentY++;
                            }
                            else if (map.GetMapPosition(currentX, currentY + 1) != deathCell)
                            {
                                currentY++;
                            }
                            else
                            {
                                response.Message = $"Suicide at {currentX}, {currentY + 1} is forbidden";
                            }

                            break;

                        case GetResponse.Directions.East:
                            if (!HasScent(currentX, currentY, map))
                            {
                                currentX++;
                            }
                            else if (map.GetMapPosition(currentX + 1, currentY) != deathCell)
                            {
                                currentX++;
                            }
                            else
                            {
                                response.Message = $"Suicide at {currentX + 1}, {currentY} is forbidden";
                            }
                            break;

                        case GetResponse.Directions.South:
                            if (!HasScent(currentX, currentY, map))
                            {
                                currentY--;
                            }
                            else if (map.GetMapPosition(currentX, currentY - 1) != deathCell)
                            {
                                currentY--;
                            }
                            else
                            {
                                response.Message = $"Suicide at {currentX}, {currentY - 1} is forbidden";
                            }
                            break;

                        case GetResponse.Directions.West:
                            if (!HasScent(currentX, currentY, map))
                            {
                                currentX--;
                            }
                            else if (map.GetMapPosition(currentX - 1, currentY) != deathCell)
                            {
                                currentX--;
                            }
                            else
                            {
                                response.Message = $"Suicide at {currentX - 1}, {currentY} is forbidden";
                            }
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    currentScent = HasScent(currentX, currentY, map);

                    if (map.GetMapPosition(currentX, currentY)
                        .Equals(deathCell, StringComparison.InvariantCultureIgnoreCase))
                    {
                        map.MarkAsScent(previousCell[0], previousCell[1]);
                        Death();
                        response.Message = $"Robot died at {previousCell[0]}, {previousCell[1]}, cell marked as {scentCell} Dangerous";
                    }
                }
            }

            response.X = currentX;
            response.Y = currentY;
            response.Scent = currentScent;
            response.Direction = currentDirection;

            return response;
        }

        public void Death()
        {
            currentX = 1;
            currentY = 1;
            currentScent = false;
            currentDirection = GetResponse.Directions.North;
        }

        private bool HasScent(int x, int y, MartianMap map)
        {
            var scent = map.GetMapPosition(x, y);
            return scent == scentCell;
        }
    }
}