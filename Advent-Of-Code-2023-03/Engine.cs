namespace AdventOfCode.Day03
{
    /// <summary>
    /// This class represents engine - grid of part numbers & part symbols
    /// </summary>
    public class Engine
    {
        public List<PartNumber> PartNumbers = [];
        public List<PartSymbol> PartSymbols = [];

        public Engine(string[] input)
        {
            for (int y = 0; y < input.Length; y++)
            {
                string line = input[y];
                int lastPartNumber = -1;
                List<int> xPositions = [];
                for (int x = 0; x < line.Length; x++)
                {
                    if (char.IsDigit(line[x]))
                    {
                        //If character is a digit, we will remember it's position, and add it at the end of lastPartNumber
                        if (lastPartNumber == -1) lastPartNumber = 0; //If we did not have last part number, we create new one
                        lastPartNumber *= 10;
                        lastPartNumber += int.Parse(line[x].ToString());
                        xPositions.Add(x);
                    }
                    else
                    {
                        if (lastPartNumber != -1)
                        {
                            //If we previously found number, we will add it into Part Numbers and reset counter
                            PartNumbers.Add(new(lastPartNumber, xPositions, y));
                            xPositions.Clear();
                            lastPartNumber = -1;
                        }
                        if (line[x] != '.')
                        {
                            //If a non-dot symbol, it is Part Symbol
                            PartSymbols.Add(new(line[x], x, y));
                        }
                    }
                }
                
                if (lastPartNumber != -1)
                { 
                    //If we found a number and it's end of engine grid, we add the number
                    PartNumbers.Add(new(lastPartNumber, xPositions, y));
                    xPositions.Clear();
                    lastPartNumber = -1;
                }
            }
        }

        /// <summary>
        /// Gets part number by x and y position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public PartNumber? GetPartNumberByPosition(int x, int y)
        {
            var gridNumbers = PartNumbers.Where((n) => { return n.Xs.Contains(x) && n.Y == y; });
            if (!gridNumbers.Any()) return null;
            return gridNumbers.First();
        }

        /// <summary>
        /// Removes part number from list. Did not require it's own function, but whatever
        /// </summary>
        /// <param name="partNumber"></param>
        public void RemovePartNumber(PartNumber? partNumber)
        {
            if (partNumber is null) return;
            PartNumbers.Remove(partNumber);
        }
    }
}