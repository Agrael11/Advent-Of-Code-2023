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

            //For each card (input line) we add it's points to total - points are number of winning cards -1
            foreach (string cardInput in inputData)
            {
                Card card = new(cardInput);
                totalPoints += (int)Math.Pow(2, card.WinningGameNumbersCount - 1);
            }

            return totalPoints;
        }
    }
}