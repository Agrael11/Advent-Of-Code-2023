namespace AdventOfCode.Day06
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
    {
        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            //Just parse the input - first line IS time, second line IS distances, remove whitespace
            //because some ELVES can't write right, and split by ':' symbol - actual number is on right
            long raceTime = long.Parse(inputData[0].Replace(" ", "").Split(':')[1]);
            long raceDistance = long.Parse(inputData[1].Replace(" ", "").Split(':')[1]);

            //Find lowest possible win hold-time
            long lowestWin = 0;
            for (long holdTime = (raceDistance/raceTime); holdTime < raceTime; holdTime++)
            {
                if ((raceTime - holdTime) * holdTime > raceDistance)
                {
                    lowestWin = holdTime;
                    break;
                }
            }

            //Find highest possible win hold-time
            long highestWin = 0;
            for (long holdTime = raceTime - (raceDistance / raceTime); holdTime > lowestWin; holdTime--)
            {
                if ((raceTime - holdTime) * holdTime > raceDistance)
                {
                    highestWin = holdTime;
                    break;
                }
            }

            //return difference (+1, because numbers yay)
            return highestWin - lowestWin+1;
        }
    }
}