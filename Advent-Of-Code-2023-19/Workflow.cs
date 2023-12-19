namespace AdventOfCode.Day19
{
    /// <summary>
    /// Implementation of "workflow"
    /// Basically big If/Else-if/Else statement with custom informations
    /// </summary>
    /// <param name="ifs"></param>
    /// <param name="elseTarget"></param>
    public readonly struct Workflow(List<(Workflow.Condition condition, string target)> ifs, string elseTarget)
    {
        public delegate bool Condition(Part input);

        public List<(Condition condition, string target)> Ifs { get; } = ifs;
        public string ElseTarget { get; } = elseTarget;
    }
}