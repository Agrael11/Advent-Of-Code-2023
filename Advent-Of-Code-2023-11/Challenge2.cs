using Helpers;

namespace AdventOfCode.Day11
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

            //Create "universe" from input, setting price of empty rows/columns, for part 2 price of empty line is 1 000 000 instead of 2
            Universe universe = new(1000000);
            foreach (string line in inputData)
            {
                List<bool> spaceLine = [];
                foreach (char c in line)
                {
                    if (c == '#') //# = galaxy.  i store it as boolean
                    {
                        spaceLine.Add(true);
                    }
                    else
                    {
                        spaceLine.Add(false);
                    }
                }
                universe.PushRow(spaceLine);
            }

            //Extend  the universe by empty columns/rows
            universe.Extend();

            //And get list of galaxies with appropriate positions after expansion
            List<Vector2l> galaxies = [];

            for (int y = 0; y < universe.Height; y++)
            {
                for (int x = 0; x < universe.Width; x++)
                {
                    if (universe.GetAt(x, y))
                    {
                        galaxies.Add(universe.GetRealPosition(x, y));
                    }
                }
            }

            //Now just count distance between each pair of galaxies
            long totalDistance = 0;

            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = i + 1; j < galaxies.Count; j++)
                {
                    totalDistance += Vector2l.SimpleDistance(galaxies[i], galaxies[j]);
                }
            }

            return totalDistance;
        }
    }
}