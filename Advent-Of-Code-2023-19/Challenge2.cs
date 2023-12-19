using System.Collections.Generic;

namespace AdventOfCode.Day19
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
            Dictionary<string, RangeWorkflow> workflows = [];

            //Little different parsing
            foreach (string inputLine in inputData)
            {
                if (string.IsNullOrWhiteSpace(inputLine))
                {
                    break;
                }

                string workflowName = inputLine[0..inputLine.IndexOf('{')];
                string[] conditions = inputLine[(inputLine.IndexOf('{')+1)..^1].Split(',');
                List<(string conditions, string target)> targets = [];
                string elseTarget = "";
                foreach (string condition in conditions) 
                { 
                    if (condition.Contains('>') || condition.Contains('<'))
                    {
                        string ifTarget = condition.Split(':')[1];
                        string ifCondition = condition.Split(':')[0];
                        targets.Add((ifCondition, ifTarget));
                    }
                    else
                    {
                        elseTarget = condition;
                    }
                }
                workflows.Add(workflowName, new RangeWorkflow(targets, elseTarget));
            }

            //We create list of part ranges with their target workflows
            List<(PartRange partRange, string target)> partRangers = [];
            partRangers.Add((new PartRange(0, 4000), "in"));

            long total = 0;

            while (partRangers.Count > 0)
            {
                (PartRange partRange, string target) = partRangers[0];
                partRangers.RemoveAt(0);

                //This basically splits the part range into multiple smaller ones by workflow definition
                var results = workflows[target].Divide(partRange);

                //if accepted, just add it's total, else if not regected add it as next partrange to explore
                foreach (var result in results)
                {
                    if (result.target == "R")
                        continue;
                    if (result.target == "A")
                    {
                        total += result.part.Total();
                        continue;
                    }
                    else
                    {
                        partRangers.Add((result.part, result.target));
                    }
                }
            }

            return total;
        }
    }
}