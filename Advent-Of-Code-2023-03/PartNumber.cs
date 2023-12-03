namespace AdventOfCode.Day03
{

    /// <summary>
    /// This class represents Part Number (any full number in engine grid)
    /// </summary>
    public class PartNumber
    {
        public readonly List<int> Xs = []; //X positions in grid it occupies
        public readonly int Y = 0;
        public readonly int Number;

        public PartNumber(int number, List<int> x, int y)
        {
            Number = number;
            Xs.AddRange(x);
            Y = y;
        }

        //Just some debug functions
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            PartNumber other = (PartNumber)obj;
            return other.Number == Number && other.Xs.Equals(Xs) && other.Y == Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Xs.GetHashCode(), Y.GetHashCode(), Number.GetHashCode());
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}