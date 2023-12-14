namespace AdventOfCode.Day14
{
    public enum BlockState { Empty, Cube, Round }
    /// <summary>
    /// Main Class for Challenge 1
    /// </summary>
    public static class Challenge1
    {
        //Info about map
        private static int mapWidth = 0;
        private static int mapHeight = 0;
        private static BlockState[,] map = new BlockState[0, 0];

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            mapWidth = inputData.Length;
            mapHeight = inputData[0].Length;
            map = new BlockState[mapWidth,mapHeight];

            //Parse input into map
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    map[x, y] = inputData[y][x] switch
                    {
                        'O' => BlockState.Round,
                        '#' => BlockState.Cube,
                        _ => BlockState.Empty,
                    };
                }
            }

            //Moves all boulders to north
            MoveBlocksNorth();

            //And returns weight on north
            return GetWeight();
        }

        /// <summary>
        /// This calculates weight on north part of mirrors
        /// </summary>
        /// <returns></returns>
        private static long GetWeight()
        {
            long weight = 0;

            for (int y = 0; y < mapHeight; y++)
            {
                int position = mapHeight - y;
                int boulders = 0;
                for (int x = 0; x < mapWidth; x++)
                {
                    if (map[x,y] == BlockState.Round) boulders++;
                }
                weight += boulders * position;
            }

            return weight;
        }

        /// <summary>
        /// Move all blocks north. just move it to last known empty place on top.
        /// </summary>
        private static void MoveBlocksNorth()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                int top = -1;
                for (int y = 0; y < mapHeight; y++)
                {
                    if (map[x, y] == BlockState.Cube)
                    {
                        top = y;
                        continue;
                    }
                    if (map[x, y] == BlockState.Round)
                    {
                        map[x, y] = BlockState.Empty;
                        map[x, top+1] = BlockState.Round;
                        top++;
                    }
                }
            }
        }
    }
}