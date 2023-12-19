namespace AdventOfCode.Day19
{
    /// <summary>
    /// One Part, but has range of states
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public struct PartRange(int min, int max)
    {
        public long MinX = min;
        public long MaxX = max;
        public long MinA = min;
        public long MaxA = max;
        public long MinM = min;
        public long MaxM = max;
        public long MinS = min;
        public long MaxS = max;

        public PartRange Clone()
        {
            PartRange part = new()
            {
                MinX = MinX,
                MinA = MinA,
                MinM = MinM,
                MinS = MinS,
                MaxX = MaxX,
                MaxA = MaxA,
                MaxM = MaxM,
                MaxS = MaxS
            };
            return part;
        }

        public long Total()
        {
            if (MaxX < MinX) MaxX = 0;
            if (MaxA < MinA) MaxA = 0;
            if (MaxM < MinM) MaxM = 0;
            if (MaxS < MinS) MaxS = 0;
            return (MaxX - MinX) * (MaxA - MinA) * (MaxM - MinM) * (MaxS - MinS);
        }

        //Just for debbuging
        public override string ToString()
        {
            return $"{Total():n0}";
        }
    }
}