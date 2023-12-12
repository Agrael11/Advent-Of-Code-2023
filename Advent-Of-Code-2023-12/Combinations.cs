using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Day12
{
    public struct Combinations(List<int> combination)
    {
        public List<int> Combination { get; set; } = combination;

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null) return false;
            if (obj is not Combinations) return false;

            return (this.Combination.SequenceEqual(((Combinations)obj).Combination));
        }

        public override int GetHashCode()
        {
            int total = 0;
            
            if (Combination.Count == 0) return -1;

            foreach (int combination in Combination)
            {
                total += combination.GetHashCode();
            }
            return total;
        }

        public static bool operator ==(Combinations a, Combinations b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Combinations a, Combinations b)
        {
            return !a.Equals(b);
        }
    }
}