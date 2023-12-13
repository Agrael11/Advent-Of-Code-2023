//Note: Thanks a lot to u/ScorixEar
//Without his hints on reddit I would end my AoC2023 Journey here

namespace AdventOfCode.Day12
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
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            List<LineInfo> lines = [];

            //Parse input data and "unfold" them
            foreach (string line in inputData)
            {
                string[] lineData = line.Split(' ');
                List<int> groups = [];
                string editedLineData0 = lineData[0] + "?" + lineData[0] + "?" + lineData[0] + "?" + lineData[0] + "?" + lineData[0];
                string editedLineData1 = lineData[1] + "," + lineData[1] + "," + lineData[1] + "," + lineData[1] + "," + lineData[1];
                foreach (string number in editedLineData1.Split(','))
                {
                    groups.Add(int.Parse(number));
                }
                lines.Add(new(groups, editedLineData0.Trim('.')));
            }

            long total = 0;

            //For each line get number of combinations
            foreach (LineInfo lineInfo in lines)
            {
                Dictionary<(int index, int group, int usedInGroup), long> memory = []; //Just a memory
                total += GetCombinations(lineInfo, 0, 0, 0, ref memory);
            }

            return total;
        }

        /// <summary>
        /// Helper function to use memory. It checks if we already checked for same thing once and if not, updates value using main combination function
        /// </summary>
        /// <param name="lineInfo"></param>
        /// <param name="index"></param>
        /// <param name="group"></param>
        /// <param name="usedInGroup"></param>
        /// <param name="memory"></param>
        /// <returns></returns>
        public static long GetCombinations(LineInfo lineInfo, int index, int group, int usedInGroup, ref Dictionary<(int index, int group, int usedInGroup), long> memory)
        {
            if (memory.ContainsKey((index, group, usedInGroup))) return memory[(index, group, usedInGroup)];

            long value = GetCombinationsWithoutMemory(lineInfo, index, group, usedInGroup, ref memory);
            memory.Add((index, group, usedInGroup), value);
            return value;
        }

        /// <summary>
        /// Search for number of possible combinations
        /// </summary>
        /// <param name="lineInfo"></param>
        /// <param name="index"></param>
        /// <param name="group"></param>
        /// <param name="usedInGroup"></param>
        /// <param name="memory"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static long GetCombinationsWithoutMemory(LineInfo lineInfo, int index, int group, int usedInGroup, ref Dictionary<(int index, int group, int usedInGroup), long> memory)
        {
            //If we ran out of characters
            //if we are done with groups system correctly, return 1 (valid combinationň
            //Otherwise return 0 (invalid combination)
            if (index == lineInfo.Line.Length)
            {
                if (group == lineInfo.Groups.Count && usedInGroup == 0)
                    return 1;

                else if (group == lineInfo.Groups.Count - 1 && usedInGroup == lineInfo.Groups[group])
                    return 1;

                else
                    return 0;
            }

            //Another check for invalid possibility. If we're out of test groups and have something in a group ... that's not right
            if (group >= lineInfo.Groups.Count && usedInGroup > 0)
                return 0;

            /*
             * If character is . AND
             * We don't have anything in group... we just move forward
             * We are out of groups, but we already find next group, we return 0 - not possible
             * Or if we found start of next group, but did not finished it, we also return 0
             * Otherwise we finished a group, and therefore we check for same character, with next group.
             */
            if (lineInfo.Line[index] == '.')
            {
                if (usedInGroup == 0)
                    return GetCombinations(lineInfo, index + 1, group, 0, ref memory);
                if (group == lineInfo.Groups.Count && usedInGroup > 0)
                    return 0;
                if (usedInGroup < lineInfo.Groups[group])
                    return 0;
                return GetCombinations(lineInfo, index + 1, group + 1, 0, ref memory);
            }
            /*
             * If we found "#" and
             * our group is out of bounds, impossible - return 0
             * length of our group is too big, also return 0
             * otherwise increment length of this group
             */
            else if (lineInfo.Line[index] == '#')
            {
                if (group == lineInfo.Groups.Count)
                    return 0;
                if (usedInGroup >= lineInfo.Groups[group])
                    return 0;
                return GetCombinations(lineInfo, index + 1, group, usedInGroup + 1, ref memory);
            }
            /*
             * For wildcard
             * If we finished groups, just move to next character without doing anything. Used to make sure there is no more # later
             * If we did not find anything in loop, we check for both possibilities - of this being a . or this being a #
             * If we have started, but haven't finished group, we pretend this is # - increase group size and move to next character
             * If we have finished this group, we pretend this is . - we move to next group and next character
             * Else this is impossible and we return 0
             */
            else if (lineInfo.Line[index] == '?')
            {
                if (group == lineInfo.Groups.Count && usedInGroup == 0)
                {
                    return GetCombinations(lineInfo, index + 1, group, 0, ref memory);
                }
                if (usedInGroup == 0)
                {
                    long splitReality = 0;
                    splitReality += GetCombinations(lineInfo, index + 1, group, 1, ref memory);
                    splitReality += GetCombinations(lineInfo, index + 1, group, 0, ref memory);
                    return splitReality;
                }
                else if (usedInGroup < lineInfo.Groups[group])
                {
                    return GetCombinations(lineInfo, index + 1, group, usedInGroup + 1, ref memory);
                }
                else if (usedInGroup == lineInfo.Groups[group])
                {
                    return GetCombinations(lineInfo, index + 1, group + 1, 0, ref memory);
                }
                else
                {
                    return 0;
                }
            }
            //This should never happen
            throw new IndexOutOfRangeException("Indexer can be only '.', '#' or '?'");
        }
    }
}