namespace AdventOfCode.Day12
{
    public struct LineInfo(List<int> groups, string line)
    {
        public List<int> Groups = groups;
        public string Line = line;
    }
}