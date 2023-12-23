using System.ComponentModel;
using System.Numerics;

namespace AdventOfCode.Day23
{
    public class Junction(int x, int y)
    {
        public int X = x;
        public int Y = y;
        public List<(Junction junction, int distance)> Connections = [];


        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode());
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Junction) return false;
            Junction other = (Junction)obj;
            return X == other.X && Y == other.Y;
        }

        public void AddConnection(Junction junction, int length)
        {
            Connections.Add((junction, length));
        }

        public static bool operator ==(Junction junction1, Junction junction2)
        {
            return junction1.Equals(junction2);
        }

        public static bool operator !=(Junction junction1, Junction junction2)
        {
            return !junction1.Equals(junction2);
        }

        public override string ToString()
        {
            return $"{{{X},{Y}}}";
        }
    }
}