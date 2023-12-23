using System.ComponentModel;
using System.Numerics;

namespace AdventOfCode.Day23
{
    public class State(int x, int y)
    {
        public int X = x;
        public int Y = y;
        public List<State> PreviousStates = [];


        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode());
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not State) return false;
            State other = (State)obj;
            return X == other.X && Y == other.Y;
        }

        public static bool operator ==(State junction1, State junction2)
        {
            return junction1.Equals(junction2);
        }

        public static bool operator !=(State junction1, State junction2)
        {
            return !junction1.Equals(junction2);
        }

        public override string ToString()
        {
            return $"{{{X},{Y}}}";
        }
    }
}