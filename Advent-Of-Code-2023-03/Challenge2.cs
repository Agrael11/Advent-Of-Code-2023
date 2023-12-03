namespace AdventOfCode.Day03
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

            Engine engine = new(inputData);
            long sumGears = 0; //We prepare our engine and final sum

            foreach (PartSymbol symbol in engine.PartSymbols)
            {
                if (symbol.Symbol != '*') continue;

                //If our part symbol is * - a gear, we get any numbers that are around it.
                List<int> gearNumbers = [];
                for (int rx = -1; rx <= 1; rx++)
                {
                    for (int ry = -1; ry <= 1; ry++)
                    {
                        if (rx == 0 && ry == 0) continue;
                        int x = symbol.X + rx;
                        int y = symbol.Y + ry;
                        var partNumber = engine.GetPartNumberByPosition(x, y);
                        if (partNumber is null) continue;
                        
                        if (!gearNumbers.Contains(partNumber.Number)) //careful not to count same number twice :)
                            gearNumbers.Add(partNumber.Number);
                    }
                }

                //If we found exactly two numbers, it is a gear - we add it's ratio to our sum.
                if (gearNumbers.Count == 2) sumGears += gearNumbers[0]*gearNumbers[1];
            }

            return sumGears;
        }
    }
}