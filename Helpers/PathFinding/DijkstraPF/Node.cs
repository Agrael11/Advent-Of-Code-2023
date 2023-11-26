using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.PathFinding.DijkstraPF
{
    public class Node<T>
    {
        public T Value { get; set; }
        public List<(Node<T> node, double distance)> ConnectedNodes { get; } = new();
        public bool Visited = false;
        public double TotalCost = double.MaxValue;
        public Node<T>? previousVisited;

        public Node(T value)
        {
            this.Value = value;
            previousVisited = null;
        }

        public override string ToString()
        {
            if (Value is null) return "Node{null}";
            return $"Node{{{Value}}}";
        }

        public override int GetHashCode()
        {
            if (Value == null) return HashCode.Combine(Visited.GetHashCode(), TotalCost.GetHashCode());
            return HashCode.Combine(Value.GetHashCode(), Visited.GetHashCode(), TotalCost.GetHashCode());
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return GetHashCode() == obj.GetHashCode();
        }
    }
}
