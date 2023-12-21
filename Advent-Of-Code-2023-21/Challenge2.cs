/*
 * Note:
 * Thanks u/nikanjX, u/bdaene and u/StatisticianJolly335 for help
 */

using Helpers;

namespace AdventOfCode.Day21
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
    {
        private static List<(int x, int y)> rocks = [];
        private static List<(int x, int y)> stepPoints = [];
        private static Dictionary<(int x, int y), List<(int x, int y)>> memory = [];
        private static int width = 0;
        private static int height = 0;
        private static readonly int Target = 26501365;

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            rocks = [];
            stepPoints = [];
            memory = [];

            width = inputData[0].Length;
            height = inputData.Length;

            for (int y = 0; y < inputData.Length; y++)
            {
                for (int x = 0; x < inputData[y].Length; x++)
                {
                    if (inputData[y][x] == '#')
                    {
                        rocks.Add((x, y));
                    }
                    else if (inputData[y][x] == 'S')
                    {
                        stepPoints.Add((x, y));
                    }
                }
            }

            int halfWidth = width / 2;

            int position = 0;
            long[] positions = [1,2,3];
            long[] results = new long[3];

            for (int i = 1; i <= width*2+halfWidth; i++)
            {
                Move();
                if (i % width == halfWidth)
                {
                    results[position] = stepPoints.Count;
                    position++;
                }
            }

            //Calculates a, b and c for quadratic formula
            long[] abc = MathHelpers.SolveSystemOfEquations(positions, results);

            return QuadraticResultForN((Target - halfWidth) / width + 1, abc[0], abc[1], abc[2]);
        }
        
        /// <summary>
        /// Calculates to get quadratic formula (a*n^2 + b*n + c)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long QuadraticResultForN(long n, long a, long b, long c)
        {
            return a * n * n + b * n + c;
        }

        /// <summary>
        /// Same as in challange1
        /// </summary>
        public static void Move()
        {
            List<(int x, int y)> newPoints = [];
            foreach (var (x, y) in stepPoints)
            {
                newPoints.AddRange(GetMoves(x, y));
            }
            stepPoints = newPoints.Distinct().ToList();
        }
        
        /// <summary>
        /// Same as in Challange 1
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static List<(int x, int y)> GetMoves(int x, int y)
        {
            if (memory.ContainsKey((x, y)))
            {
                return memory[(x, y)];
            }

            List<(int x, int y)> moves = [];
            if (Possible(x - 1, y)) moves.Add((x - 1, y));
            if (Possible(x + 1, y)) moves.Add((x + 1, y));
            if (Possible(x, y - 1)) moves.Add((x, y - 1));
            if (Possible(x, y + 1)) moves.Add((x, y + 1));
            memory.Add((x, y), moves);
            return moves;
        }

        /// <summary>
        /// This now returns info for items outside of grid instead as marking them as unreachable
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool Possible(int x, int y)
        {
            while (x < 0) x += width;
            while (y < 0) y += height;
            while (x >= width) x -= width;
            while (y >= height) y -= height;
            return !rocks.Contains((x, y));
        }
    }
}