using Helpers;
using System.Drawing;

namespace AdventOfCode.Day10
{
    /// <summary>
    /// Main Class for Challenge 1
    /// </summary>
    public static class Challenge1
    {
        static Vector2i gridSize = new(0,0);
        static char[,] gridData = new char[0,0];

        //Just a shortcut :)
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
            Vector2i StartPosition = new(0,0);

            //we just parse the input. if input character is S it's start tile
            for (int y = 0; y < inputData.Length; y++)
            {
                for (int x = 0; x < inputData[y].Length; x++)
                {
                    gridData[x,y] = inputData[y][x];
                    if (inputData[y][x] == 'S')
                    {
                        StartPosition.Y = y;
                        StartPosition.X = x;
                    }
                }
            }

            //Define connections of start pipe, previous pipe (start position) and current pipe (first connected pipe to start pipe)
            Vector2i[] startConnections = GetStartConnections(StartPosition);
            Vector2i previous = new(StartPosition);
            Vector2i current = new(startConnections[0]);
            int length = 1;

            //and we simply go along the loop by "instructions" until we get back to start.
            while (GetAt(current.X, current.Y) != 'S')
            {
                length++;
                Vector2i next = GetNext(previous, current);
                previous = current;
                current = next;
            } 

            //most distant point is the one exactly in middle of loop
            return length/2;
        }

        /// <summary>
        /// Gets connecting pipes to start pipe.
        /// Just check around the pipe. If it connects to starting pipe add it as starting point.
        /// Note: I am assuming there are always exactly two valid connected pipes. If in fact there were more, 
        /// you would have to check if they are connecting back to start pipe.
        /// </summary>
        /// <param name="startPosition"></param>
        /// <returns></returns>
        public static Vector2i[] GetStartConnections(Vector2i startPosition)
        {
            Vector2i[] connections = new Vector2i[2];
            int point = 0;
            //this just check every pipe around - made it into dictionary so it's easier.
            foreach (string key in positionalOffsets.Keys)
            {
                int x = startPosition.X + positionalOffsets[key].X;
                int y = startPosition.Y + positionalOffsets[key].Y;
                char c = GetAt(x, y);
                switch (key) //And depending to direction check if the pipe can actually connect
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

        /// <summary>
        /// Similar to previous, but gets two connected pipes by the "instruction" of the pipe.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets next pipe connected to current pipe. Previous pipe is to easily rule it out.
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public static Vector2i GetNext(Vector2i previous, Vector2i current)
        {
            Vector2i[] connections = GetConnections(current);
            if (connections[0] == previous) return connections[1];
            return connections[0];
        }

        /// <summary>
        /// Gets character in grid. if not in grid return '.' as if it was in it.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static char GetAt(int x, int y)
        {
            if (x < 0 || y < 0 || x >= gridSize.X || y >= gridSize.Y) return '.';
            return (gridData[x, y]);
        }
    }
}