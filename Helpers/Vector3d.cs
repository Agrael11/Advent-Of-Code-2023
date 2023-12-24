using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Vector3d
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        public Vector3d(Vector3d vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Vector3d) return false;

            return X == ((Vector3d)obj).X && Y == ((Vector3d)obj).Y && Z == ((Vector3d)obj).Z;
        }

        public static double SimpleDistance(Vector3d a, Vector3d b)
        {
            return Math.Abs(a.X-b.X) + Math.Abs(a.Y-b.Y) + Math.Abs(a.Z-b.Z);
        }

        public static bool operator ==(Vector3d a, Vector3d b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector3d a, Vector3d b)
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
