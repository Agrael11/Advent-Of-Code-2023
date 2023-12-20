using Helpers;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdventOfCode.Day20
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
    {
        private static List<(bool pulse, string target)> broadcast = [];
        private static List<(string source, bool pulse, string target)> signals = [];
        private static Dictionary<string, Module> modules = [];
        private static string trueTarget = "";

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            broadcast = [];
            signals = [];
            modules = [];

            //Same parsing as before
            foreach (string inputLine in inputData)
            {
                string[] moduleInfo = inputLine.Replace(" ", "").Split("->");
                string[] targetModules = moduleInfo[1].Split(',');
                if (moduleInfo[0] == "broadcaster")
                {
                    foreach (string targetModule in targetModules)
                    {
                        broadcast.Add((false, targetModule));
                    }
                }
                else if (moduleInfo[0][0] == FlipFlop.Identity)
                {
                    modules.Add(moduleInfo[0][1..], new FlipFlop([.. targetModules]));
                }
                else if (moduleInfo[0][0] == Conjuction.Identity)
                {
                    modules.Add(moduleInfo[0][1..], new Conjuction([.. targetModules]));
                }
                else
                {
                    throw new Exception("Invalid input");
                }
            }

            //Just as in part 1, but if target is "rx" we remember this as it's connected conjuction module
            foreach (string moduleKey in modules.Keys)
            {
                foreach (string target in modules[moduleKey].Targets)
                {
                    if (target == "rx")
                    {
                        trueTarget = moduleKey;
                    }
                    if (modules.TryGetValue(target, out Module? value) && value is Conjuction conjuction)
                    {
                        conjuction.AddInput(moduleKey);
                    }
                }
            }

            /*
             * The target conjuction module is connected to separate loops.
             * We check how many times each loop has to go through
             */

            List<long> totalNeeded = [];

            foreach (var (pulse, target) in broadcast)
            {
                ResetModules();
                long attempts = 1;
                while (!PressButton(target, trueTarget))
                {
                    attempts++;
                }
                totalNeeded.Add(attempts);
            }

            //And get lowest common multiple of results

            while (totalNeeded.Count > 1)
            {
                totalNeeded[0] = MathHelpers.LeastCommonMultiple(totalNeeded[0], totalNeeded[1]);
                totalNeeded.RemoveAt(1);
            }

            return totalNeeded[0];
        }

        /// <summary>
        /// Resets module to original state
        /// </summary>
        public static void ResetModules()
        {
            foreach (var module in modules.Values)
            {
                module.Reset();
            }
        }

        /// <summary>
        /// Very similar to original "PressButton", but instead checks if target got high signal exactly once
        /// </summary>
        /// <param name="startpoint"></param>
        /// <param name="targetPoint"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool PressButton(string startpoint, string targetPoint)
        {
            signals.Add(("", false, startpoint));

            int resultPresses = 0;
            while (signals.Count > 0)
            {
                var (source, pulse, target) = signals[0];
                signals.RemoveAt(0);
                if (target == targetPoint)
                {
                    if (pulse == true) resultPresses++;
                }
                if (modules.TryGetValue(target, out Module? value))
                {
                    foreach (var result in value.ContinuePulse(source, pulse))
                    {
                        signals.Add((target, result.pulse, result.target));
                    }
                }
            }
            return resultPresses == 1;
            throw new Exception("?");
        }
    }
}