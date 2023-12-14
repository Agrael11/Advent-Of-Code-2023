namespace AdventOfCode.Day13
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
        /// Checks if all lines are mirrored around same pivot point, but returns true only if at least one error was found
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private static bool LinesMirrored(List<string> lines, int position)
        {
            //Keeps track if smudge was removed, which is mandatory
            bool smudged = false;
            foreach (string line in lines)
            {
                if (!LineMirrored(line, position, ref smudged)) return false;
            }
            if (!smudged) return false;
            return true;
        }

        /// <summary>
        /// Check if single line is mirrored around pivot point
        /// </summary>
        /// <param name="line"></param>
        /// <param name="position"></param>
        /// <param name="smudged"></param>
        /// <returns></returns>
        private static bool LineMirrored(string line, int position, ref bool smudged)
        {
            string part1 = line[..position];
            string part2 = new(line[position..].Reverse().ToArray());
            //If we already found smudge on previous lines, we do standard comparison
            if (smudged)
            {
                return part1.EndsWith(part2) || part2.EndsWith(part1);
            }
            else
            {
                //If we find smudged mirroring we modify "smudge" value and return true
                if (EndsWithError(part1, part2) || EndsWithError(part2, part1))
                {
                    smudged = true;
                    return true;
                }
                else return part1.EndsWith(part2) || part2.EndsWith(part1);
                //Otherwise we do standard comparison
            }
        }

        /// <summary>
        /// Custom Ends With that requires one error :)
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <returns></returns>
        private static bool EndsWithError(string line1, string line2)
        {
            bool error = false;
            if (line2.Length > line1.Length) return false;
            for (int i = 0; i < line2.Length; i++)
            {
                if (line1[^(1 + i)] != line2[^(1 + i)] && error == true) return false;
                else if (line1[^(1 + i)] != line2[^(1 + i)]) error = true;
            }
            return error;
        }
    }
}