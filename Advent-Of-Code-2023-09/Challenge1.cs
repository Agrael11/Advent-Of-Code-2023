namespace AdventOfCode.Day09
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

            //Total of "next" values in sequences
            int total = 0;

            //foreach each line of input (one line = one sequence)
            foreach (string inputLine in inputData)
            {
                //we parse numbers to create a Sequence
                string[] numbers = inputLine.Split(' ');
                Sequence sequence = new();
                foreach (string number in numbers)
                {
                    sequence.AddNumberAtEnd(int.Parse(number.Trim()));
                }
                
                //We add next value by pattern
                sequence.AddNextByPattern();

                //Get last value of sequence and add it to total
                int? last = sequence.GetLast();

                if (last is not null)
                {
                    total += last.Value;
                }
            }

            //Too easy
            return total;
        }
    }
}