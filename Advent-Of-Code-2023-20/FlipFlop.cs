namespace AdventOfCode.Day20
{
    /// <summary>
    /// Flip-flop version of module - if false is received, switches itself and passes it's value to connected modules
    /// </summary>
    /// <param name="targets"></param>
    public class FlipFlop(List<string> targets) : Module(targets)
    {
        public static readonly char Identity = '%';
        public bool State { get; set; } = false;

        public override List<(bool pulse, string target)> ContinuePulse(string source, bool pulse)
        {
            if (pulse == true) return [];

            State = !State;

            List<(bool pulse, string target)> output = [];
            foreach (string target in Targets)
            {
                output.Add((State, target));
            }

            return output;
        }

        public override void Reset()
        {
            State = false;
        }
    }
}