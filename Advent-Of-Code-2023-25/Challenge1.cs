namespace AdventOfCode.Day25
{
    /// <summary>
    /// Main Class for Challenge 1
    /// </summary>
    public static class Challenge1
    {
        private static Random random = new();
        
        private static List<string> modules = [];
        private static List<(string node1, string node2)> edges = [];

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');
            random = new();

            modules = [];
            edges = [];

            foreach (string line in inputData)
            {
                string[] data = line.Split(':');
                string[] connections = data[1].TrimStart(' ').Split(' ');

                if (!modules.Contains(data[0]))
                {
                    modules.Add(data[0]); 
                }

                foreach (string connection in connections)
                {
                    if (!modules.Contains(connection))
                    {
                        modules.Add(connection);
                    }

                    if (!edges.Contains((data[0], connection)) && !edges.Contains((connection, data[0])))
                    {
                        edges.Add((data[0], connection));
                    }
                }
            }

            return KerbalAlgorithm();
        }

        private static int KerbalAlgorithm()
        {
            List<List<string>> subsets;
            do
            {
                subsets = [];
                foreach (string module in modules)
                {
                    subsets.Add([module]);
                }

                List<string> set1 = [];
                List<string> set2 = [];

                while (subsets.Count > 2)
                {
                    int randomEdge = random.Next(0, edges.Count);
                    set1 = subsets.Where(s => s.Contains(edges[randomEdge].node1)).First();
                    set2 = subsets.Where(s => s.Contains(edges[randomEdge].node2)).First();
                    if (set1 == set2) continue;
                    subsets.Remove(set2);
                    set1.AddRange(set2);
                }
            } while (CountCuts(subsets) != 3); //check

            return subsets.Aggregate(1, (p, s) => p * s.Count);
        }

        private static int CountCuts(List<List<string>> subsets)
        {
            int cuts = 0;
            for (int i = 0; i < edges.Count; ++i)
            {
                var subset1 = subsets.Where(s => s.Contains(edges[i].node1)).First();
                var subset2 = subsets.Where(s => s.Contains(edges[i].node2)).First();
                if (subset1 != subset2) ++cuts;
            }

            return cuts;
        }
    }
}