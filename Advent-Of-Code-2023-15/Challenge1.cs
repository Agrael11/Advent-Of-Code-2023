namespace AdventOfCode.Day15
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
            //string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');
            string inputData = input.Replace("\r", "").Replace("\n", "");

            //for each comma divided item add it's hash to total
            int total = 0;
            foreach (string hash in inputData.Split(','))
            {
                total += CalculateHash(hash);
            }

            return total;
        }

        /// <summary>
        /// Simply hash calculation.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int CalculateHash(string str)
        {
            int value = 0;
            foreach (char c in str)
            {
                value += (int)c;
                value *= 17;
            }
            value &= 0xFF;
            return value;
        }
    }
}