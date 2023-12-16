using System.Threading.Tasks;
using System;

namespace AdventOfCode.Day16
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
    {
        static int width = 0;
        static int height = 0;
        static char[,] map = new char[0, 0];
        static readonly List<(int x, int y)> visited = [];
        static readonly List<(int x, int y, int xvel, int yvel)> memory = [];
        static readonly List<(int x, int y, int xvel, int yvel)> tasks = [];

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');
            memory.Clear();
            visited.Clear();

            width = inputData[0].Length;
            height = inputData.Length;
            map = new char[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = inputData[y][x];
                }
            }

            int largest = 0;

            //Check every possible start point, and if result is larger keep it
            for (int x = 0; x < width; x++)
            {
                int result = StartDirection(x, 0, 0, 1);
                if (result > largest) largest = result;
                result = StartDirection(x, height - 1, 0, -1);
                if (result > largest) largest = result;
            }

            for (int y = 0; y < height; y++)
            {
                int result = StartDirection(0, y, 1, 0);
                if (result > largest) largest = result;
                result = StartDirection(width - 1, y, -1, 0);
                if (result > largest) largest = result;
            }

            return largest;
        }

        /// <summary>
        /// Starts movement of beam in direction and returns number of places it crosses
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="velocityX"></param>
        /// <param name="velocityY"></param>
        /// <returns></returns>
        static int StartDirection(int X, int Y, int velocityX, int velocityY)
        {
            visited.Clear();
            memory.Clear();
            tasks.Clear();
            MoveBeam(X, Y, velocityX, velocityY);
            while (tasks.Count != 0)
            {
                (int x, int y, int velX, int velY) = tasks[^1];
                tasks.RemoveAt(tasks.Count - 1);
                MoveBeam(x, y, velX, velY);
            }
            return visited.Count;
        }

        /// <summary>
        /// Moves beam of light from point in given direction
        /// Creates new task(s) as needed
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="velocityX"></param>
        /// <param name="velocityY"></param>
        static void MoveBeam(int X, int Y, int velocityX, int velocityY)
        {
            //If we already went this direction from this place we would be just repeating same thing
            if (memory.Contains((X, Y, velocityX, velocityY))) return;
            memory.Add((X, Y, velocityX, velocityY));

            //If Out of bounds
            if (X < 0 || X >= width || Y < 0 || Y >= height) return;

            //If we haven't visited this cell yet
            if (!visited.Contains((X, Y)))
            {
                visited.Add((X, Y));
            }

            switch (map[X, Y])
            {
                case '.': //Find next point to move to.
                    while (true)
                    {
                        X += velocityX;
                        Y += velocityY;
                        if (X < 0 || X >= width || Y < 0 || Y >= height) break;
                        if (map[X, Y] != '.' || !(map[X, Y] == '|' && velocityX == 0) || !(map[X, Y] == '-' && velocityY == 0)) break;
                        if (!visited.Contains((X, Y)))
                        {
                            visited.Add((X, Y));
                        }
                        break;
                    }
                    tasks.Add((X, Y, velocityX, velocityY));
                    break;
                case '\\': //Switch direction
                    if (velocityX == 1)
                    {
                        tasks.Add((X, Y + 1, 0, 1));
                        break;
                    }
                    if (velocityX == -1)
                    {
                        tasks.Add((X, Y - 1, 0, -1));
                        break;
                    }
                    if (velocityY == 1)
                    {
                        tasks.Add((X + 1, Y, 1, 0));
                        break;
                    }
                    tasks.Add((X - 1, Y, -1, 0));
                    break;
                case '/': //Switch direction
                    if (velocityX == 1)
                    {
                        tasks.Add((X, Y - 1, 0, -1));
                        break;
                    }
                    if (velocityX == -1)
                    {
                        tasks.Add((X, Y + 1, 0, 1));
                        break;
                    }
                    if (velocityY == 1)
                    {
                        tasks.Add((X - 1, Y, -1, 0));
                        break;
                    }
                    tasks.Add((X + 1, Y, 1, 0));
                    break;
                case '-': //Split horizontally
                    tasks.Add((X - 1, Y, -1, 0));
                    tasks.Add((X + 1, Y, 1, 0));
                    break;
                case '|': //Split Vertically
                    tasks.Add((X, Y - 1, 0, -1));
                    tasks.Add((X, Y + 1, 0, 1));
                    break;
            }
        }
    }
}