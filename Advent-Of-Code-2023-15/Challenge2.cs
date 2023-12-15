namespace AdventOfCode.Day15
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
            //string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');
            string inputData = input.Replace("\r", "").Replace("\n", "");

            Dictionary<string, int> lenses = [];
            Dictionary<int, List<string>> boxes = [];

            //For each comma divided definition
            foreach (string lensCode in inputData.Split(','))
            {
                //If it's remove instruction
                if (lensCode.EndsWith('-'))
                {
                    //We get it's box and if it is in it, we remove it from box
                    int hash = CalculateHash(lensCode[..^1]);
                    if (boxes.TryGetValue(hash, out List<string>? value))
                    {
                        if (value.Contains(lensCode[..^1]))
                        {
                            value.Remove(lensCode[..^1]);
                        }
                    }
                }
                else
                {
                    //If it's assign instruction, we find the box, and add or replace
                    string lens = lensCode.Split('=')[0];
                    int hash = CalculateHash(lens);
                    int focus = int.Parse(lensCode.Split("=")[1]);

                    if (!lenses.TryAdd(lens, focus))
                    {
                        lenses[lens] = focus;
                    }

                    if (boxes.TryGetValue(hash, out List<string>? value))
                    {
                        if (!value.Contains(lens))
                        {
                            value.Add(lens);
                        }
                    }
                    else
                    {
                        boxes.Add(hash, [lens]);
                    }
                }
            }

            int total = 0;

            foreach (int key in boxes.Keys)
            {
                for (int slot = 0; slot < boxes[key].Count; slot++)
                {
                    string lens = boxes[key][slot];
                    total += (key + 1) * (slot + 1) * lenses[lens];
                }
            }

            return total;
        }


        /// <summary>
        /// Simply hash calculation.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int CalculateHash(string str)
        {
            int value = 0;
            foreach (char c in str)
            {
                value += (int)c;
                value *= 17;
            }
            value &= 0xFF;
            return value;
        }
    }
}