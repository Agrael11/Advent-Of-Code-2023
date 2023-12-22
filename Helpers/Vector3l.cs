using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Vector3l
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public Vector3l(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        public Vector3l(Vector3l vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Vector3l) return false;

            return X == ((Vector3l)obj).X && Y == ((Vector3l)obj).Y && Z == ((Vector3l)obj).Z;
        }

        public static long SimpleDistance(Vector3l a, Vector3l b)
        {
            return Math.Abs(a.X-b.X) + Math.Abs(a.Y-b.Y) + Math.Abs(a.Z-b.Z);
        }

        public static bool operator ==(Vector3l a, Vector3l b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector3l a, Vector3l b)
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
