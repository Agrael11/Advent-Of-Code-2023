namespace AdventOfCode.Day01
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
    {
        static readonly List<(int value, string text)> numbers = [
        (1, "1"), (2, "2"), (3, "3"), (4, "4"), (5, "5"),
        (6, "6"), (7, "7"), (8, "8"), (9, "9"),
        (1, "one"), (2, "two"), (3, "three"),
        (4, "four"), (5, "five"), (6, "six"),
        (7, "seven"), (8, "eight"), (9, "nine") ];

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static int DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            int totalCalibrationValue = 0;
            //For each line
            for (int line = 0; line < inputData.Length; line++)
            {
                //Get First Digit * 10  and Second Digit
                int calibrationValue = GetFirstDigit(inputData[line]) * 10 + GetLastDigit(inputData[line]);
                totalCalibrationValue += calibrationValue;
            }
            //And return the last item (biggest)
            return totalCalibrationValue;
        }

        //Gets first digit in the string
        public static int GetFirstDigit(string str)
        {
            int first = int.MaxValue;
            int outp = -1;
            foreach (var (value, text) in numbers)
            {
                int index = str.IndexOf(text);
                if (index >= 0 && index < first)
                {
                    first = index;
                    outp = value;
                }
            }
            return outp;
        }

        //Gets last digit in the string
        public static int GetLastDigit(string str)
        {
            int first = int.MinValue;
            int outp = -1;
            foreach (var (value, text) in numbers)
            {
                int index = str.LastIndexOf(text);
                if (index >= 0 && index > first)
                {
                    first = index;
                    outp = value;
                }
            }
            return outp;
        }
    }
}