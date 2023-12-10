using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Vector2i
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }


        public Vector2i(Vector2i vector)
        {
            X = vector.X;
            Y = vector.Y;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Vector2i) return false;

            return X == ((Vector2i)obj).X && Y == ((Vector2i)obj).Y;
        }

        public static bool operator ==(Vector2i a, Vector2i b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector2i a, Vector2i b)
        {
            return !a.Equals(b);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode());
        }

        public override string ToString()
        {
            return $"({X}-{Y})";
        }
    }
}
