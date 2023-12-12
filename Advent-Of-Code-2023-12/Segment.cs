using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Day12
{
    public struct Segment(List<Combinations> possibilities)
    {
        public List<Combinations> Possibilities = possibilities;

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null) return false;
            if (obj is not Segment) return false;

            return (this.Possibilities.SequenceEqual(((Segment)obj).Possibilities));
        }

        public override int GetHashCode()
        {
            int total =0;
            foreach (Combinations Possibility in Possibilities)
            {
                total += Possibility.GetHashCode();
            }
            return total;
        }

        public static bool operator ==(Segment a, Segment b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Segment a, Segment b)
        {
            return !a.Equals(b);
        }
    }
}