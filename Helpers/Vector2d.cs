using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Vector2d
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2d(double x, double y)
        {
            X = x;
            Y = y;
        }


        public Vector2d(Vector2d vector)
        {
            X = vector.X;
            Y = vector.Y;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Vector2d) return false;

            return X == ((Vector2d)obj).X && Y == ((Vector2d)obj).Y;
        }

        public static double SimpleDistance(Vector2d a, Vector2d b)
        {
            return Math.Abs(a.X-b.X) + Math.Abs(a.Y-b.Y);
        }

        public static bool operator ==(Vector2d a, Vector2d b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector2d a, Vector2d b)
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
