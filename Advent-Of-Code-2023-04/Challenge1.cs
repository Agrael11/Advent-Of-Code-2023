namespace AdventOfCode.Day04
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
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            int totalPoints = 0;

            foreach (string cardInput in inputData)
            {
                Card card = new(cardInput);
                int winningNumbersCount = card.WinningGameNumbers.Count;
                int cardPoints = (int)Math.Pow(2, winningNumbersCount - 1);
                totalPoints += cardPoints;
            }

            return totalPoints;
        }
    }
}