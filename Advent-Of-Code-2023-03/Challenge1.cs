using System;

namespace AdventOfCode.Day03
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

            Engine engine = new(inputData); //We create engine from input
            long sumPartNumbers = 0; //And prepare final sum

            foreach (PartSymbol partSymbol in engine.PartSymbols)
            {
                for (int rx = -1; rx <= 1; rx++)
                {
                    for (int ry = -1; ry <= 1; ry++)
                    {
                        //We look around the symbol for a part number
                        if (rx == 0 && ry == 0) continue;

                        int x = partSymbol.X + rx;
                        int y = partSymbol.Y + ry;
                        var partNumber = engine.GetPartNumberByPosition(x, y);
                        if (partNumber is null) continue; //If no part number was found, continue with another place around symbol

                        //If we did find part number, we will add it to our sum, and remove it from list so it doesn't count twice
                        sumPartNumbers += partNumber.Number; 
                        engine.RemovePartNumber(partNumber);
                    }
                }
            }

            return sumPartNumbers;
        }
    }
}