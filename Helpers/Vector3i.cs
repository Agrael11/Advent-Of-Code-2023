using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Vector3i
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector3i(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        public Vector3i(Vector3i vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Vector3i) return false;

            return X == ((Vector3i)obj).X && Y == ((Vector3i)obj).Y && Z == ((Vector3i)obj).Z;
        }

        public static int SimpleDistance(Vector3i a, Vector3i b)
        {
            return Math.Abs(a.X-b.X) + Math.Abs(a.Y-b.Y) + Math.Abs(a.Z-b.Z);
        }

        public static bool operator ==(Vector3i a, Vector3i b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector3i a, Vector3i b)
        {
            return !a.Equals(b);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode());
        }

        public override string ToString()
        {
            return $"({X}-{Y}-{Z})";
        }
    }
}
