namespace AdventOfCode.Day05
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
        public static ulong DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            List<ulong> seeds = [];

            //Get information about seeds
            string[] seedsInfo = inputData[0].Split(' ');
            for (int i = 1; i < seedsInfo.Length; i ++)
            {
                seeds.Add(ulong.Parse(seedsInfo[i]));
            }

            //Generate mappings from input Data
            Dictionary<string, AlmanacMap> maps = AlmanacMap.GenerateMaps(inputData);

            ulong lowestLocation = ulong.MaxValue;

            //maps seeds to locations
            foreach (ulong seed in seeds)
            {
                ulong statusValue = seed;
                string seedStatus = "seed";
                //Step by step try to map current seedValue, by current seedStatus (seed, location, humidity, etc.) to next one.
                while (seedStatus != "location")
                {
                    (statusValue, seedStatus) = Map(statusValue, seedStatus, ref maps);
                }

                if (statusValue < lowestLocation) lowestLocation = statusValue; //Just find lowest one.
            }

            return lowestLocation;
        }


        /// <summary>
        /// Returns next sourcestatus and sourcevalue from current ones.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="source"></param>
        /// <param name="maps"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static (ulong value, string destination) Map(ulong value, string source, ref Dictionary<string, AlmanacMap> maps)
        {
            try
            {
                return (maps[source].GetMapped(value), maps[source].Destination);
            }
            catch (Exception ex)
            {
                throw new Exception("Source not found", ex);
            }
        }
    }
}