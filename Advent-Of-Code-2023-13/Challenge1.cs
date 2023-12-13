namespace AdventOfCode.Day13
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

            List<string> rows = [];
            List<string> columns = [];

            long total = 0;

            //For each line of input
            foreach (string line in inputData)
            {
                //If we're on empty line, we ended one ... ehm... map part?
                if (string.IsNullOrWhiteSpace(line))
                {
                    total += GetMirroredValue(rows, columns); //So we check for reflection and get the values

                    //And reset so we can continue with next part of map
                    rows.Clear();
                    columns.Clear();
                    continue;
                }

                //Just add this line as row, and make columns into lines so it's easier to parse
                rows.Add(line);

                for (int i = 0; i < line.Length; i++)
                {
                    if (columns.Count <= i)
                    {
                        columns.Add(line[i].ToString());
                    }
                    else
                    {
                        columns[i] = columns[i] + line[i];
                    }
                }
            }

            //Just for the last map...part...thingy - there's no empty line after that, so we do it once more after loop (but I have check just in case)
            if (rows.Count > 0 && columns.Count > 0)
            {
                total += GetMirroredValue(rows, columns);
            }

            //Returns total
            return total;
        }

        /// <summary>
        /// Gets mirrored value of map, with rows and columns and returns their value
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private static long GetMirroredValue(List<string> rows, List<string> columns)
        {
            // Basically check whether rows are mirrored vertically (along columns)
            for (int i = 1; i < columns.Count; i++)
            {
                if (LinesMirrored(rows, i))
                {
                    return i;
                }
            }
            // And same for columns, but horizontally (along rows)
            for (int i = 1; i < rows.Count; i++)
            {
                if (LinesMirrored(columns, i))
                {
                    return i * 100;
                }
            }
            // If not there's no mirroring, which should be impossible?
            throw new Exception("Not mirrored");
        }

        /// <summary>
        /// Checks if all lines are mirrored at pivot point
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private static bool LinesMirrored(List<string> lines, int position)
        {
            foreach (string line in lines)
            {
                if (!LineMirrored(line, position)) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if one line is mirrored around pivot point - basically take line, split it, reverse one split part and check if they overlap.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private static bool LineMirrored(string line, int position)
        {
            string part1 = line.Substring(0, position);
            string part2 = new(line.Substring(position).Reverse().ToArray());
            return part1.EndsWith(part2) || part2.EndsWith(part1);
        }
    }
}