namespace AdventOfCode.Day07
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static partial class Challenge2
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

            //Convert inputs to hands
            List<JokerHand> hands = [];
            foreach (string inputLine in inputData)
            {
                hands.Add(new JokerHand(inputLine));
            }

            //Sort hands
            hands.Sort(JokerHand.Compare);

            //Add up bids multiplied by strength of hand
            long totalWinnings = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                totalWinnings += (i + 1) * hands[i].Bid;
            }

            return totalWinnings;
        }
    }
}