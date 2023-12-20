namespace AdventOfCode.Day20
{
    /// <summary>
    /// Conuction module - returns false if all inputs are true
    /// </summary>
    /// <param name="targets"></param>
    public class Conjuction(List<string> targets) : Module(targets)
    {
        public Dictionary<string, bool> States = [];
        public static readonly char Identity = '&';
        private int statesHelper = 0;

        /// <summary>
        /// Adds possilbe input
        /// </summary>
        /// <param name="input"></param>
        public override void AddInput(string input)
        {
            States.TryAdd(input, false);
        }

        /// <summary>
        /// Resets state
        /// </summary>
        public override void Reset()
        {
            foreach (string state in States.Keys)
            {
                States[state] = false;
            }
            statesHelper = 0;
        }

        /// <summary>
        /// Gets list of signals it sends based on source input. Also remembers inputs.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pulse"></param>
        /// <returns></returns>
        public override List<(bool pulse, string target)> ContinuePulse(string source, bool pulse)
        {
            bool oldState = States[source];
            States[source] = pulse;
            if (oldState == true && States[source] == false)
            {
                statesHelper--;
            }
            if (oldState == false && States[source] == true)
            {
                statesHelper++;
            }

            bool returnPulse = !(statesHelper == States.Count);

            List<(bool pulse, string target)> output = [];
            foreach (string target in Targets)
            {
                output.Add((returnPulse, target));
            }

            return output;
        }
    }
}