namespace AdventOfCode.Day01
{
    /// <summary>
    /// Main Class for Challange 1
    /// </summary>
    public static class Challange1
    {
        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static int DoChallange(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');
            //Remember total CalibratioN value
            int totalCalibrationValue = 0;
            //For each LIne
            for (int line = 0; line < inputData.Length; line++)
            {
                //Get first digit (and multiply by 10
                int calibrationValue = 0;
                for (int characterPos = 0; characterPos < inputData[line].Length; characterPos++)
                {
                    char character = inputData[line][characterPos];
                    if (IsDigit(character))
                    {
                        calibrationValue = (character - '0') * 10;
                        break;
                    }
                }
                //Get last digit
                for (int characterPos = inputData[line].Length-1; characterPos >= 0; characterPos--)
                {
                    char character = inputData[line][characterPos];
                    if (IsDigit(character))
                    {
                        calibrationValue += (character - '0');
                        break;
                    }
                }
                totalCalibrationValue += calibrationValue;
            }
            //And return the total calibration value
            return totalCalibrationValue;
        }

        /// <summary>
        /// Check whether character is a digit
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static bool IsDigit(char character)
        {
            return (character >= '0' && character <= '9');
        }
    }
}