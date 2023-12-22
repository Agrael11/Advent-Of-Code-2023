using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023_22
{
    internal class ListComparer<T> : IEqualityComparer<List<T>>
    {
        public bool Equals(List<T>? x, List<T>? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return x.SequenceEqual(y);
        }

        public int GetHashCode(List<T> obj)
        {
            int hashCode = 0;
            foreach (T t in obj)
            {
                if (t is not null)
                hashCode ^= t.GetHashCode();
            }

            return hashCode;
        }
    }
}