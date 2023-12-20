namespace AdventOfCode.Day20
{
    /// <summary>
    /// Simple abstract Module
    /// </summary>
    /// <param name="targets"></param>
    public abstract class Module(List<string> targets)
    {
        public List<string> Targets { get; set; } = targets;

        public virtual void AddInput(string input) { }
        public abstract List<(bool pulse,string target)> ContinuePulse(string source, bool pulse);
        public abstract void Reset();
    }
}