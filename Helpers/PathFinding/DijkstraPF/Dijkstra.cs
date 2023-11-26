using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.PathFinding.DijkstraPF
{
    public class Dijkstra<T>
    {

        private Node<T> _currentNode;
        private List<Node<T>> _nodesToCheck = new();
        public delegate bool EndTest(Node<T> node);
        private EndTest _endTest;
        private bool _descending;

        public Dijkstra(Node<T> startingNode, EndTest isEnd, bool descending = false)
        {
            _currentNode = startingNode;
            startingNode.TotalCost = 0;
            _endTest = isEnd;
            _descending = descending;
        }

        public (bool found, Node<T>? lowest) DoOneStep()
        {
            foreach (var node in _currentNode.ConnectedNodes)
            {
                if (!node.node.Visited)
                {
                    double nextCost = _currentNode.TotalCost + node.distance;
                    if (nextCost < node.node.TotalCost)
                    {
                        node.node.TotalCost = nextCost;
                        node.node.previousVisited = _currentNode;
                        if (_endTest(node.node))
                        {
                            return (true, node.node);
                        }
                        if (!_nodesToCheck.Contains(node.node)) _nodesToCheck.Add(node.node);
                        _currentNode.Visited = true;
                    }
                }
            }
            _nodesToCheck.Remove(_currentNode);
            if (_nodesToCheck.Count == 0)
            {
                return (false, null);
            }
            if (!_descending)
            {
                _nodesToCheck = _nodesToCheck.OrderBy(node => node.TotalCost).ToList();
            }
            else
            {
                _nodesToCheck = _nodesToCheck.OrderByDescending(node => node.TotalCost).ToList();
            }
            _currentNode = _nodesToCheck[0];
            return (false, _currentNode);
        }

        public static Node<T>? DoDijkstra(Node<T> startingNode, EndTest isEnd, bool descending = false)
        {
            Dijkstra<T> dijkstra = new(startingNode, isEnd, descending);
            Node<T>? latest = null;
            Node<T>? check;
            bool result;
            do
            {
                (result, check) = dijkstra.DoOneStep();
                if (check != null) latest = check;
            } while (!result && check != null);
            return latest;
        }
    }
}
