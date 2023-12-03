namespace AdventOfCode.Day03
{
    /// <summary>
    /// This class represents Part Symbol (any non-dot symbol in engine grid)
    /// </summary>
    public class PartSymbol(char symbol, int x, int y)
    {
        public readonly int X = x;
        public readonly int Y = y;
        public readonly char Symbol = symbol;


        //Just some debug functions
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            PartSymbol other = (PartSymbol)obj;
            return other.Symbol == Symbol && other.X == X && other.Y == Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Symbol.GetHashCode());
        }
    }
}