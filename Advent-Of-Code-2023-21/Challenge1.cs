namespace AdventOfCode.Day21
{
    /// <summary>
    /// Main Class for Challenge 1
    /// </summary>
    public static class Challenge1
    {
        private static List<(int x, int y)> rocks = [];
        private static List<(int x, int y)> stepPoints = [];
        private static Dictionary<(int x, int y), List<(int x, int y)>> memory = [];
        private static int width = 0;
        private static int height = 0;

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

            for (int i = 0; i < 64; i++)
            {
                Move();
            }

            return stepPoints.Count;
        }

        /// <summary>
        /// Tries to move every piece orthogonally and removes duplicates
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
        /// Gets possible moves of piece
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static List<(int x, int y)>GetMoves(int x, int y)
        {
            if (memory.ContainsKey((x,y)))
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
        /// Checks if position is valid (Not a rock and not out of grid)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool Possible(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height) return false;
            return !rocks.Contains((x,y));
        }
    }
}