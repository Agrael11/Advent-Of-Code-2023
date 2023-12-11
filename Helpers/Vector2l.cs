using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Vector2l
    {
        public long X { get; set; }
        public long Y { get; set; }

        public Vector2l(long x, long y)
        {
            X = x;
            Y = y;
        }


        public Vector2l(Vector2l vector)
        {
            X = vector.X;
            Y = vector.Y;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Vector2l) return false;

            return X == ((Vector2l)obj).X && Y == ((Vector2l)obj).Y;
        }

        public static long SimpleDistance(Vector2l a, Vector2l b)
        {
            return Math.Abs(a.X-b.X) + Math.Abs(a.Y-b.Y);
        }

        public static bool operator ==(Vector2l a, Vector2l b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector2l a, Vector2l b)
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
