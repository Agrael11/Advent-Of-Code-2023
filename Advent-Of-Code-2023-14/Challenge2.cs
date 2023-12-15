using System.Globalization;
using System.IO.MemoryMappedFiles;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;

namespace AdventOfCode.Day14
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
    {
        private static int mapWidth;
        private static int mapHeight;
        private static readonly MirrorMap map = new(0,0);
        private static readonly List<int> states = [];

        private readonly static int cycles = 1000000000;

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
            map.Reset(mapWidth, mapHeight);

            //Parse input into map
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    map[y * mapWidth + x] = inputData[y][x] switch
                    {
                        'O' => BlockState.Round,
                        '#' => BlockState.Cube,
                        _ => BlockState.Empty,
                    };
                }
            }

            //This remembers loop information. This helps us to know when we are looping in same results
            List<int> loop = [];

            //For each cycle
            for (int cycle = 0; cycle < cycles; cycle++)
            {
                //We check if we already found this state
                int state = ContainsState();
                if (state >= 0)
                {
                    //If we did, we check if this state is part of loop
                    if (loop.Contains(state))
                    {
                        //if this cycle would be final if we were following loop we found our weight
                        if ((cycles - cycle) % loop.Count == 0)
                        {
                            break;
                        }
                        else
                        {
                            //otherwise we move to next state by faking Hash
                            MoveBlocks();
                        }
                    }
                    else
                    {
                        //If it's not part of loop, we add this state as part of loop AND
                        //we fake next hash of map (so we don't have to calculate it again)
                        loop.Add(state);
                        map.HashCache = states[state + 1];
                    }
                }
                else //If we did not found this state already
                {
                    MoveBlocks(); //Calculate next one and save it to list of states
                    states.Add(map.GetHashCode());
                }
            }

            //In the end we return weight on north
            return GetWeight();
        }

        /// <summary>
        /// Calculates Weight on North
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
                    if (map[y * mapWidth + x] == BlockState.Round) boulders++;
                }
                weight += boulders * position;
            }

            return weight;
        }

        /// <summary>
        /// Checks if states list contains current state (except last one, which IS current state)
        /// </summary>
        /// <returns></returns>
        private static int ContainsState()
        {
            for (int i = 0; i < states.Count-1; i++)
            {
                if (states[i] == map.GetHashCode()) return i;
            }
            return -1;
        }

        /// <summary>
        /// Does one Cycle - moves blocks north, west, south, east
        /// </summary>
        private static void MoveBlocks()
        {
            MoveBlocksNorth();
            MoveBlocksWest();
            MoveBlocksSouth();
            MoveBlocksEast();
        }

        /// <summary>
        /// Moves blocks north ...
        /// </summary>
        private static void MoveBlocksNorth()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                int top = -1;
                for (int y = 0; y < mapHeight; y++)
                {
                    if (map[y * mapWidth + x] == BlockState.Cube)
                    {
                        top = y;
                        continue;
                    }
                    if (map[y * mapWidth + x] == BlockState.Round)
                    {
                        map[y * mapWidth + x] = BlockState.Empty;
                        map[(top + 1)* mapWidth + x] = BlockState.Round;
                        top++;
                    }
                }
            }
        }

        /// <summary>
        /// ... south ...
        /// </summary>
        private static void MoveBlocksSouth()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                int bottom = mapHeight;
                for (int y = mapHeight - 1; y >= 0; y--)
                {
                    if (map[y *mapWidth + x] == BlockState.Cube)
                    {
                        bottom = y;
                        continue;
                    }
                    if (map[y * mapWidth + x] == BlockState.Round)
                    {
                        map[y * mapWidth + x] = BlockState.Empty;
                        map[(bottom - 1) * mapWidth + x] = BlockState.Round;
                        bottom--;
                    }
                }
            }
        }

        /// <summary>
        /// ... east ...
        /// </summary>
        private static void MoveBlocksEast()
        {
            for (int y = 0; y < mapHeight; y++)
            {
                int right = mapWidth;
                for (int x = mapWidth-1; x >= 0; x--)
                {
                    if (map[y * mapWidth + x] == BlockState.Cube)
                    {
                        right = x;
                        continue;
                    }
                    if (map[y * mapWidth + x] == BlockState.Round)
                    {
                        map[y * mapWidth + x] = BlockState.Empty;
                        map[y * mapWidth + right - 1] = BlockState.Round;
                        right--;
                    }
                }
            }
        }

        /// <summary>
        /// ... and west
        /// </summary>
        private static void MoveBlocksWest()
        {
            for (int y = 0; y < mapHeight; y++)
            {
                int left= -1;
                for (int x = 0; x < mapWidth; x++)
                {
                    if (map[y * mapWidth + x] == BlockState.Cube)
                    {
                        left = x;
                        continue;
                    }
                    if (map[y * mapWidth + x] == BlockState.Round)
                    {
                        map[y * mapWidth + x] = BlockState.Empty;
                        map[y * mapWidth + left + 1] = BlockState.Round;
                        left++;
                    }
                }
            }
        }
    }
}