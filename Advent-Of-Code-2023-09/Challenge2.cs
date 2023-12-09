namespace AdventOfCode.Day09
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
        public static int DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            //Everything is just like day 1
            int total = 0;

            foreach (string inputLine in inputData)
            {
                string[] numbers = inputLine.Split(' ');
                Sequence sequence = new();
                foreach (string number in numbers)
                {
                    sequence.AddNumberAtEnd(int.Parse(number.Trim()));
                }
            
                //Except we're adding previous value by pattern and work with that
                sequence.AddPreviousByPattern();

                int? first = sequence.GetFirst();

                if (first is not null)
                {
                    total += first.Value;
                }
            }

            return total;
        }
    }
}