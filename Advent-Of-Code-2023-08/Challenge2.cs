/* 
    NOTE/RANT
    I am not a fan of how this challenge was made at all.
    This solution works with assumption that for each start point, the distance from that start point to 
    first Z and that distance from any Z to next Z in that start point is same when using instructions

    That means that if input did not mysteriously work with that "rule" the output of this code will not be correct
*/

namespace AdventOfCode.Day08
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

            //Parse instructions and node map
            char[] instructions = inputData[0].Trim().ToCharArray();

            Dictionary<string, PathNode> nodes = [];
            List<string> currentNodes = [];

            for (int i = 2; i < inputData.Length; i++)
            {
                string[] inputSplit = inputData[i].Split('=');
                string nodeName = inputSplit[0].Trim();
                string[] nodesData = inputSplit[1].Trim().Split(',');
                string leftNode = nodesData[0].Trim().TrimStart('(');
                string rightNode = nodesData[1].Trim().TrimEnd(')');
                nodes.Add(nodeName, new(leftNode, rightNode));
                if (nodeName.EndsWith('A')) currentNodes.Add(nodeName);
            }

            //Remember number of steps for each starting point
            List<long> steps = [];

            //For each startting node we do same thing as first day, just remember the number of steps for each of them.
            for (int currentNode = 0; currentNode < currentNodes.Count; currentNode++)
            {
                long nodeSteps = 0;
                int instruction = 0;
                while (!currentNodes[currentNode].EndsWith('Z'))
                {
                    switch (instructions[instruction])
                    {
                        case 'L': currentNodes[currentNode] = nodes[currentNodes[currentNode]].LeftConnection; break;
                        case 'R': currentNodes[currentNode] = nodes[currentNodes[currentNode]].RightConnection; break;
                    }
                    nodeSteps++;
                    instruction++;
                    instruction %= instructions.Length;
                }
                steps.Add(nodeSteps);
            }

            //And we get least common multiple of those numbers
            return GetLeastCommonMultiple(steps);
        }

        /// <summary>
        /// Gets least common multiple of list of numbers.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        static long GetLeastCommonMultiple(List<long> numbers)
        {
            if (numbers.Count == 0) return 0;
            if (numbers.Count == 1) return numbers[0];
            long multiple = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                multiple = Helpers.MathHelpers.LeastCommonMultiple(multiple, numbers[i]);
            }
            return multiple;
        }
    }
}