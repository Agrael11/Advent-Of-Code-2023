using System.Data;
using System.Numerics;
using System.Xml;

namespace AdventOfCode.Day12
{

    /// <summary>
    /// Main Class for Challenge 1
    /// </summary>
    public static class Challenge1
    {
        //I really don't know why this doesn't work...
        //Works on example input
        //Real input result too low :/



        private readonly static Dictionary<string, Segment> knownSegments = [];

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            List<LineInfo> lineInfos = [];

            foreach (string line in inputData)
            {
                string[] lineInfo = line.Split();
                List<int> results = [];
                string[] requiredSegments = lineInfo[1].Split(',');
                foreach (string requiredSegment in requiredSegments)
                {
                    results.Add(int.Parse(requiredSegment));
                }

                string[] subsegments = lineInfo[0].Split('.', StringSplitOptions.RemoveEmptyEntries);
                List<Segment> thisLineSegments = [];
                foreach (string subsegment in subsegments)
                {
                    
                    thisLineSegments.Add(GetCombinations(subsegment));
                }
                lineInfos.Add(new (results, thisLineSegments));
            }

            long total = 0;

            int ll = 0;
            StreamWriter writer = new("temp.txt");
            foreach (LineInfo lineInfo in lineInfos)
            {
                long found = GetPossibleSegments(lineInfo.Segments, lineInfo.RequiredResult);
                writer.WriteLine($"{inputData[ll]}   {found}");
                total += found;
                ll++;
            }
            writer.Close();

            return total;
        }

        /// <summary>
        /// Gets number of possible segments for each possible combinations in that segment.
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="targets"></param>
        /// <param name="segmentIndex"></param>
        /// <param name="targetIndex"></param>
        /// <returns></returns>
        public static long GetPossibleSegments(List<Segment> segments, List<int> targets, int segmentIndex = 0, int targetIndex = 0)
        {
            if (segmentIndex >= segments.Count)
            {
                if (targetIndex == targets.Count)
                    return 1;
                else
                    return 0;
            }
            long possibleCount = 0;
            Dictionary<Combinations, long> checkedPossibilities = [];
            foreach (Combinations possibility in segments[segmentIndex].Possibilities)
            {
                if (checkedPossibilities.ContainsKey(possibility))
                {
                    possibleCount += checkedPossibilities[possibility];
                    continue;
                }

                bool possible = true;
                for (int i = 0; i < possibility.Combination.Count; i++)
                {
                    if (targetIndex + i >= targets.Count)
                    {
                        possible = false;
                        break;
                    }
                    if (possibility.Combination[i] != targets[targetIndex + i])
                    {
                        possible = false;
                        break;
                    }
                }
                if (possible)
                {
                    long countPossible = GetPossibleSegments(segments, targets, segmentIndex + 1, targetIndex + possibility.Combination.Count);
                    possibleCount += countPossible;
                    checkedPossibilities.Add(possibility, countPossible);
                }
            }
            
            return possibleCount;
        }

        /// <summary>
        /// Gets all combinations for segment in number form
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        private static Segment GetCombinations(string segment)
        {
            if (knownSegments.TryGetValue(segment, out Segment value))
            {
                return value;
            }

            List<Combinations> possibleCombinations = [];
            List<string> stringCombinations = GenerateCombinations(segment.ToCharArray());

           // Console.WriteLine();
            foreach (string stringCombination in stringCombinations)
            {
                List<int> combination = [];
                int length = 0;
                foreach (char c in stringCombination)
                {
                    if (c == '.' && length > 0)
                    {
                        combination.Add(length);
                        length = 0;
                    }
                    if (c == '#') length++;
                }
                if (length > 0)
                {
                    combination.Add(length);
                }

                possibleCombinations.Add(new(combination));
                //Console.WriteLine(stringCombination);
            }

            Segment segmentPossibilities = new(possibleCombinations);

            knownSegments.Add(segment, segmentPossibilities);
            return segmentPossibilities;
        }

        /// <summary>
        /// Generates all combinations for segment but in text form
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="index"></param>
        /// <param name="combinations"></param>
        /// <returns></returns>
        static List<string> GenerateCombinations(char[] chars, int index = 0, List<string>? combinations = null)
        {
            combinations ??= [];

            if (index == chars.Length)
            {
                combinations.Add(new string(chars));
                return combinations;
            }

            if (chars[index] == '?')
            {
                chars[index] = '.';
                GenerateCombinations(chars, index + 1, combinations);

                chars[index] = '#';
                GenerateCombinations(chars, index + 1, combinations);

                chars[index] = '?'; // Backtrack
            }
            else
            {
                GenerateCombinations(chars, index + 1, combinations);
            }
            return combinations;
        }
    }
}