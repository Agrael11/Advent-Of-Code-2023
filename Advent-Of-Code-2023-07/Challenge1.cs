using System;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode.Day07
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

            //Convert inputs to hands
            List<Hand> hands = [];
            foreach (string inputLine in inputData)
            {
                hands.Add(new Hand(inputLine));
            }

            //Sort hands
            hands.Sort(Hand.Compare);

            //Add up bids multiplied by strength of hand
            int totalWinnings = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                totalWinnings += (i + 1) * hands[i].Bid;
            }

            return totalWinnings;
        }
    }
}