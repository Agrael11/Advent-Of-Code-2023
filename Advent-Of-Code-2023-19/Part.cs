namespace AdventOfCode.Day19
{
    /// <summary>
    /// Single Part
    /// </summary>
    /// <param name="x"></param>
    /// <param name="m"></param>
    /// <param name="a"></param>
    /// <param name="s"></param>
    public struct Part(int x, int m, int a, int s)
    {
        public int X { get; set; } = x;
        public int M { get; set; } = m;
        public int A { get; set; } = a;
        public int S { get; set; } = s;

        public readonly long Rating
        {
            get
            {
                return X + M + A + S;
            }
        }
    }
}