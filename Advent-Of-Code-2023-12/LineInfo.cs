using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Day12
{
    public struct LineInfo(List<int> requiredResult, List<Segment> segments)
    {
        public List<int> RequiredResult = requiredResult;
        public List<Segment> Segments = segments;

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null) return false;
            if (obj is not LineInfo) return false;

            return (this.Segments.SequenceEqual(((LineInfo)obj).Segments) && this.RequiredResult.SequenceEqual(((LineInfo)obj).RequiredResult));
        }

        public override int GetHashCode()
        {
            int total = 0;
            foreach (int Required in RequiredResult)
            {
                total += Required.GetHashCode();
            }
            foreach (Segment Segment in Segments)
            {
                total += Segment.GetHashCode();
            }

            return total;
        }

        public static bool operator ==(LineInfo a, LineInfo b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(LineInfo a, LineInfo b)
        {
            return !a.Equals(b);
        }
    }
}