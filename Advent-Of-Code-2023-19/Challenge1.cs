namespace AdventOfCode.Day19
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

            bool parsingFunction = true;
            Dictionary<string, Workflow> workflows = [];
            List<Part> parts = [];

            //Parse input
            foreach (string inputLine in inputData)
            {
                if (string.IsNullOrWhiteSpace(inputLine))
                {
                    parsingFunction = false;
                    continue;
                }

                if (parsingFunction)
                {
                    var (name, workflow) = ParseFunction(inputLine);
                    workflows.Add(name, workflow);
                    continue;
                }

                parts.Add(ParsePart(inputLine[1..^1]));
            }

            long total = 0;

            //Run each part through workflow
            foreach (Part part in parts)
            {
                string target = "in";
                //If it's not R (rejected) or A (accepted), run it through the specified workflow
                while (target != "R" && target != "A")
                {
                    Workflow workflow = workflows[target];
                    bool found = false;
                    foreach (var comparison in workflow.Ifs)
                    {
                        if (comparison.condition(part))
                        {
                            target = comparison.target;
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        target = workflow.ElseTarget;
                    }
                }
                if (target == "A")
                {
                    //If next workflow target is A it's Accepted Part
                    total += part.Rating;
                }
            }

            return total;
        }

        public static Part ParsePart(string input)
        {
            string[] datas = input.Split(',');
            Part part = new();
            foreach (string data in datas)
            {
                string variableName = data.Split('=')[0];
                int variableValue = int.Parse(data.Split('=')[1]);
                switch (variableName)
                {
                    case "x":
                        part.X = variableValue;
                        break;
                    case "m":
                        part.M = variableValue;
                        break;
                    case "a":
                        part.A = variableValue;
                        break;
                    case "s":
                        part.S = variableValue;
                        break;
                    default:
                        throw new Exception("?");
                }
            }
            return part;
        }

        public static (string name, Workflow workflow) ParseFunction(string input)
        {
            string functionName = input[..input.IndexOf('{')];
            string[] functionInfos = input[(input.IndexOf('{') + 1)..^1].Split(',');
            List<(Workflow.Condition condition, string target)> targets = [];
            for (int i = 0; i < functionInfos.Length-1; i++)
            {
                string[] functioninfo = functionInfos[i].Split(":");
                Workflow.Condition condition = GenerateConditionFunction(functioninfo[0]);
                string target = functioninfo[1];
                targets.Add((condition, target));
            }
            return (functionName, new(targets, functionInfos[^1]));
        }

        public static Workflow.Condition GenerateConditionFunction(string condition)
        {
            string partType = "";
            int number = 0;
            if (condition.Contains('<'))
            {
                partType = condition.Split('<')[0];
                number = int.Parse(condition.Split('<')[1]);
                switch (partType)
                {
                    case "x":
                        return (Part state) => { return (state.X < number); };
                    case "m":
                        return (Part state) => { return (state.M < number); };
                    case "a":
                        return (Part state) => { return (state.A < number); };
                    case "s":
                        return (Part state) => { return (state.S < number); };
                }
            }
            else
            {
                partType = condition.Split('>')[0];
                number = int.Parse(condition.Split('>')[1]);
                switch (partType)
                {
                    case "x":
                        return (Part state) => { return (state.X > number); };
                    case "m":
                        return (Part state) => { return (state.M > number); };
                    case "a":
                        return (Part state) => { return (state.A > number); };
                    case "s":
                        return (Part state) => { return (state.S > number); };
                }
            }
            throw new Exception("?");
        }
    }
}