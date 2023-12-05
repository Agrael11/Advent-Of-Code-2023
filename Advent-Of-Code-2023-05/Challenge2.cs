namespace AdventOfCode.Day05
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
        public static ulong DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            //Read seeds as ranges
            List<CustomRange> seedRanges = [];
            string[] seedsInfo = inputData[0].Split(' ');
            for (int i = 1; i < seedsInfo.Length; i+=2)
            {
                ulong rangeStart = ulong.Parse(seedsInfo[i]);
                ulong rangeLength = ulong.Parse(seedsInfo[i + 1]);
                seedRanges.Add(new CustomRange(rangeStart, rangeStart + rangeLength-1));
            }
            seedRanges = CustomRange.TryCombine(seedRanges);


            //Parse input as maps
            Dictionary<string, AlmanacMap> maps = AlmanacMap.GenerateMaps(inputData);

            
            string seedStatus = "seed";

            //Until we get to location, remap SeedValues into next status
            while (seedStatus != "location")
            {
                List<CustomRange> newRanges = [];
                AlmanacMap map = maps[seedStatus];
                foreach (CustomRange r in seedRanges)
                {
                    newRanges.AddRange(map.MapRange(r));
                }
                seedRanges = CustomRange.TryCombine(newRanges);
                seedStatus = map.Destination;
            }

            return seedRanges[0].Start;
        }
    }
}