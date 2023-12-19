namespace AdventOfCode.Day19
{
    /// <summary>
    /// Workflow that is able to divide part into ranges by conditions.
    /// </summary>
    /// <param name="ifs"></param>
    /// <param name="elseTarget"></param>
    public struct RangeWorkflow(List<(string conditions, string target)> ifs, string elseTarget)
    {
        public readonly List<(string conditions, string target)> Ifs = ifs;
        public string ElseTarget = elseTarget;

        public readonly List<(PartRange part, string target)> Divide(PartRange part)
        {
            List<(PartRange part, string target)> results = [];
            PartRange elsePart = part.Clone();
            foreach ((string condition, string target) in Ifs)
            {
                if (condition.Contains('<'))
                {
                    string variableName = condition.Split('<')[0];
                    int variableTarget = int.Parse(condition.Split('<')[1]);
                    PartRange newPart = elsePart.Clone();
                    switch (variableName)
                    {
                        case "x":
                            if (newPart.MaxX >= variableTarget)
                            {
                                newPart.MaxX = variableTarget - 1;
                                elsePart.MinX = variableTarget - 1;
                            }
                            break;
                        case "a":
                            if (newPart.MaxA >= variableTarget)
                            {
                                newPart.MaxA = variableTarget - 1;
                                elsePart.MinA = variableTarget - 1;
                            }
                            break;
                        case "s":
                            if (newPart.MaxS >= variableTarget)
                            {
                                newPart.MaxS = variableTarget - 1;
                                elsePart.MinS = variableTarget - 1;
                            }
                            break;
                        case "m":
                            if (newPart.MaxM >= variableTarget)
                            {
                                newPart.MaxM = variableTarget - 1;
                                elsePart.MinM = variableTarget - 1;
                            }
                            break;
                    }
                    results.Add((newPart, target));
                }
                else
                {
                    string variableName = condition.Split('>')[0];
                    int variableTarget = int.Parse(condition.Split('>')[1]);
                    PartRange newPart = elsePart.Clone();
                    switch (variableName)
                    {
                        case "x":
                            newPart.MinX = variableTarget;
                            elsePart.MaxX = variableTarget;
                            break;
                        case "a":
                            newPart.MinA = variableTarget;
                            elsePart.MaxA = variableTarget;
                            break;
                        case "s":
                            newPart.MinS = variableTarget;
                            elsePart.MaxS = variableTarget;
                            break;
                        case "m":
                            newPart.MinM = variableTarget;
                            elsePart.MaxM = variableTarget;
                            break;
                    }
                    results.Add((newPart, target));
                }
            }
            results.Add((elsePart, ElseTarget));
            return results;
        }
    }
}