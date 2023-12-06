using System;

namespace AdventOfCode.Day06
{
    /// <summary>
    /// Main Class for Challenge 1
    /// </summary>
    public static class Challenge1
    {
        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static int DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            //Just parse the input - first line are times, second line are distances, split by empty space ...
            string[] times = inputData[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] distances = inputData[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            //...  and convert to time-distance pairs
            List<(int time, int distance)> races = [];
            for (int i = 1; i < times.Length; i++)
            {
                races.Add((int.Parse(times[i]), int.Parse(distances[i])));
            }

            int totalWins = 1;

            //And now for every race we test every possibility ... and then multiply wins with number of possible inputs.
            foreach ((int raceTime, int raceDistance) in races)
            {
                int possibleWins = 0;
                for (int holdTime = (raceDistance / raceTime); holdTime < raceTime - (raceDistance / raceTime);  holdTime++)
                {
                    if ((raceTime - holdTime) * holdTime > raceDistance) possibleWins++;
                }
                totalWins *= possibleWins;
            }

            return totalWins;
        }
    }
}