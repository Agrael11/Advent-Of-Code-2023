namespace AdventOfCode.Day08
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
        public static int DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            //read instruction set
            char[] instructions = inputData[0].Trim().ToCharArray();

            //Parse the map of nodes.
            Dictionary<string, PathNode> nodes = [];

            for (int i = 2; i < inputData.Length; i++)
            {
                string[] inputSplit = inputData[i].Split('=');
                string nodeName = inputSplit[0].Trim();
                string[] nodesData = inputSplit[1].Trim().Split(',');
                string leftNode = nodesData[0].Trim().TrimStart('(');
                string rightNode = nodesData[1].Trim().TrimEnd(')');
                nodes.Add(nodeName, new(leftNode, rightNode));
            }

            int instruction = 0;
            int steps = 0;
            string currentNode = "AAA";
            string targetNode = "ZZZ";

            //Until we did not get to our target
            while (currentNode != targetNode)
            {
                //Move left or right, depending on instruction
                switch (instructions[instruction])
                {
                    case 'L': currentNode = nodes[currentNode].LeftConnection; break;
                    case 'R': currentNode = nodes[currentNode].RightConnection; break;
                }
                //Increase number of steps and current instruction
                steps++;
                instruction++;
                //But make sure our instruction pointer is within bounds of instructions
                instruction %= instructions.Length;
            }

            return steps;
        }
    }
}