using System.Collections.Generic;
using System.IO.Compression;

namespace AdventOfCode.Day20
{
    /// <summary>
    /// Main Class for Challenge 1
    /// </summary>
    public static class Challenge1
    {
        private static List<(bool pulse, string target)> broadcast = [];
        private static List<(string source, bool pulse, string target)> signals = [];
        private static Dictionary<string, Module> modules = [];

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

            //Very simple Parsing
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

            //This just adds possible inputs to conjuction modules - it allows it to check if all are true
            foreach (string moduleKey in modules.Keys)
            {
                foreach (string target in modules[moduleKey].Targets)
                {
                    if (modules.TryGetValue(target, out Module? value) && value is Conjuction conjuction)
                    {
                        conjuction.AddInput(moduleKey);
                    }
                }
            }

            long lowPulses = 0;
            long highPulses = 0;
            //Tries to press 1000 times. remember how many singlas were sent
            for (int i = 0; i < 1000; i++)
            {
                (long low, long high) = PressButton();
                lowPulses += low;
                highPulses += high;
            }

            return highPulses*lowPulses;
        }

        /// <summary>
        /// Single press of button
        /// </summary>
        /// <returns></returns>
        public static (long low, long high) PressButton()
        {
            long lowPulses = 1;
            long highPulses = 0;

            //Send signal from button to every start module
            foreach (var (pulse, target) in broadcast)
            {
                signals.Add(("", pulse, target));
            }

            //For every signal we have send it to module and add signals sent from module to list
            while (signals.Count > 0)
            {
                var (source, pulse, target) = signals[0];
                signals.RemoveAt(0);
                if (pulse == true) highPulses++;
                else lowPulses++;
                if (modules.TryGetValue(target, out Module? value))
                {
                    foreach (var result in value.ContinuePulse(source, pulse))
                    {
                        signals.Add((target, result.pulse, result.target));
                    }
                }
            }

            return (lowPulses, highPulses);
        }
    }
}