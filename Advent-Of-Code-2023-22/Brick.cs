using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023_22
{
    internal class Brick (Vector3i position, Vector3i position2)
    {
        public Vector3i Position = position;
        public Vector3i Position2 = position2;
        public Vector3i Size
        {
            get
            {
                return new(Position2.X - Position.X + 1, Position2.Y - Position.Y + 1, Position2.Z - Position.Z + 1);
            }
        }

        public readonly List<Brick> Supports = [];
        public readonly List<Brick> SupportedBy = [];

        public bool Intersects(Brick other)
        {
            return (this.Position.X <= other.Position2.X &&
                this.Position2.X >= other.Position.X &&
                this.Position.Y <= other.Position2.Y &&
                this.Position2.Y >= other.Position.Y &&
                this.Position.Z <= other.Position2.Z &&
                this.Position2.Z >= other.Position.Z);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Brick) return false;

            Brick other = (Brick)obj;
            return Position.Equals(other.Position) && Position2.Equals(other.Position2);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position.GetHashCode(), Position2.GetHashCode());
        }
    }
}
