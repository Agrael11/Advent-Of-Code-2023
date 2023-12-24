using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Vector2f
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2f(float x, float y)
        {
            X = x;
            Y = y;
        }


        public Vector2f(Vector2f vector)
        {
            X = vector.X;
            Y = vector.Y;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Vector2f) return false;

            return X == ((Vector2f)obj).X && Y == ((Vector2f)obj).Y;
        }

        public static float SimpleDistance(Vector2f a, Vector2f b)
        {
            return Math.Abs(a.X-b.X) + Math.Abs(a.Y-b.Y);
        }

        public static bool operator ==(Vector2f a, Vector2f b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector2f a, Vector2f b)
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
