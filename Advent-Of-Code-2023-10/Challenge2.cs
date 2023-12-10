using Helpers;
using System.ComponentModel;
using System.Net;
using System.Runtime.ExceptionServices;

namespace AdventOfCode.Day10
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
    {
        static Vector2i gridSize = new(0, 0);
        static char[,] gridData = new char[0, 0];

        //Same shortcut
        static readonly Dictionary<string, Vector2i> positionalOffsets = new()
        { { "west", new Vector2i(-1, 0) }, { "east", new Vector2i(1, 0) }, { "north", new Vector2i(0, -1) }, { "south", new Vector2i(0, 1) } };

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static int DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');
            gridSize = new Vector2i(inputData[0].Length, inputData.Length);
            gridData = new char[gridSize.X, gridSize.Y];
            Vector2i StartPosition = new(0, 0);

            //Parse input, startPosition = S tile
            for (int y = 0; y < inputData.Length; y++)
            {
                for (int x = 0; x < inputData[y].Length; x++)
                {
                    gridData[x, y] = inputData[y][x];
                    if (inputData[y][x] == 'S')
                    {
                        StartPosition.Y = y;
                        StartPosition.X = x;
                    }
                }
            }

            //We cleanup everything and and replace original grid with loop only (and updated 'S') therefore everything that is not part of loop is '\0'
            Vector2i[] startConnections = GetStartConnections(StartPosition);
            Vector2i previous = new(StartPosition);
            Vector2i current = new(startConnections[0]);

            char[,] newGrid = new char[gridSize.X, gridSize.Y];
            newGrid[StartPosition.X, StartPosition.Y] = GetAt(StartPosition.X, StartPosition.Y);
            while (gridData[current.X, current.Y] != 'S')
            {
                newGrid[current.X, current.Y] = GetAt(current.X, current.Y);
                Vector2i next = GetNext(previous, current);
                previous = current;
                current = next;
            }
            gridData = newGrid;

            int tiles = 0;

            /*
            This checks if any non-loop part of grid is a tile enclosed by loop
            
            To simplify - if there is even number of vertical loop movement, when checking from left to right, it's not enclosed by the loop
            ...|..|...
            We're not in loop, then we go through | and we are in loop then we go through | again and we're out of loop
            
            Now to handle turns I have to give thanks to u/sunnyjum for hint on reddit
            
            Don't know how i did not realize it, but when loop goes F---7 it means we went to this row from bottom and exited back bottom - therefore we never entered loop
            So I have to account for that. if we come from bottom and exit up it is vertical movement, otherwise it's not
            */
            for (int y = 0; y < gridSize.Y; y++)
            {
                for (int x = 0; x < gridSize.X; x++)
                {
                    if (newGrid[x,y] == '\0')
                    {
                        bool inLoop = false;
                        char last = '\0';
                        for (int x2 = x+1; x2 <  gridSize.X; x2++)
                        {
                            char thisChar = newGrid[x2, y];
                            if (thisChar == '\0' || thisChar == '-')
                                continue;
                            if (thisChar == '|') 
                            {
                                inLoop = !inLoop;
                                continue;
                            }
                            if (last == '\0')
                            {
                                last = thisChar;
                                continue;
                            }
                            if (last == '7' && thisChar != 'F' || last == 'F' && thisChar != '7')
                            {
                                inLoop = !inLoop;
                            }
                            if (last == 'L' && thisChar != 'J' || last == 'J' && thisChar != 'L')
                            {
                                inLoop = !inLoop;
                            }
                            last = '\0';
                        }
                        if (inLoop)
                        {
                            tiles++;
                        }
                    }
                }
            }

            return tiles;
        }

        //Same as at Day 1
        public static Vector2i[] GetStartConnections(Vector2i startPosition)
        {
            Vector2i[] connections = new Vector2i[2];
            int point = 0;
            foreach (string key in positionalOffsets.Keys)
            {
                int x = startPosition.X + positionalOffsets[key].X;
                int y = startPosition.Y + positionalOffsets[key].Y;
                char c = GetAt(x, y);
                switch (key)
                {
                    case "east":
                        if (c is 'J' or '7' or '-')
                        {
                            connections[point] = new(x, y);
                            point++;
                        }
                        break;
                    case "west":
                        if (c is 'L' or 'F' or '-')
                        {
                            connections[point] = new(x, y);
                            point++;
                        }
                        break;
                    case "north":
                        if (c is 'F' or '7' or '|')
                        {
                            connections[point] = new(x, y);
                            point++;
                        }
                        break;
                    case "south":
                        if (c is 'J' or 'L' or '|')
                        {
                            connections[point] = new(x, y);
                            point++;
                        }
                        break;
                }
            }
            return connections;
        }

        //Same as at Day 1
        public static Vector2i[] GetConnections(Vector2i position)
        {
            char c = GetAt(position.X, position.Y);
            return c switch
            {
                '|' => [
                        new (position.X, position.Y-1),
                        new (position.X, position.Y+1),
                        ],
                '-' => [
                        new (position.X-1, position.Y),
                        new (position.X+1, position.Y),
                        ],
                'L' => [
                        new (position.X, position.Y-1),
                        new (position.X+1, position.Y),
                        ],
                'J' => [
                        new (position.X, position.Y-1),
                        new (position.X-1, position.Y),
                        ],
                '7' => [
                        new (position.X, position.Y+1),
                        new (position.X-1, position.Y),
                        ],
                'F' => [
                        new (position.X, position.Y+1),
                        new (position.X+1, position.Y),
                        ],
                _ => [],
            };
        }


        //Same as at Day 1
        public static Vector2i GetNext(Vector2i previous, Vector2i current)
        {
            Vector2i[] connections = GetConnections(current);
            if (connections[0] == previous) return connections[1];
            return connections[0];
        }

        /// <summary>
        /// This method is little different to Day 1 counterpart - it replaces 'S' with correct symbol
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static char GetAt(int x, int y)
        {
            if (x < 0 || y < 0 || x >= gridSize.X || y >= gridSize.Y) return '.';
            if (gridData[x,y] == 'S')
            {
                Vector2i[] startConnection = GetStartConnections(new Vector2i(x, y));
                if (startConnection[0].X == startConnection[1].X) return '-';
                if (startConnection[0].Y == startConnection[1].Y) return '|';
                if (startConnection[0].X < startConnection[1].X)
                {
                    if (startConnection[0].Y < startConnection[1].Y)
                    {
                        if (startConnection[1].Y == y) return 'L';
                        return '7';
                    }
                    if (startConnection[0].Y > startConnection[1].Y)
                    {
                        if (startConnection[1].Y == y) return 'L';
                        return '7';
                    }
                }
                if (startConnection[0].X > startConnection[1].X)
                {
                    if (startConnection[0].Y < startConnection[1].Y)
                    {
                        if (startConnection[1].Y == y) return 'J';
                        return 'F';
                    }
                    if (startConnection[0].Y > startConnection[1].Y)
                    {
                        if (startConnection[1].Y == y) return 'J';
                        return 'F';
                    }
                }
            }
            return (gridData[x, y]);
        }
    }
}