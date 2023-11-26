using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.PathFinding.AStarPF
{
    public class AStar<T>
    {

        private Node<T> _currentNode;
        private List<Node<T>> _nodesToCheck = new();
        public delegate bool EndTest(Node<T> node);
        private EndTest _endTest;
        private bool _descending;

        public AStar(Node<T> startingNode, EndTest isEnd, bool descending = false)
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
                    double nextCost = _currentNode.TotalCost + node.node.ExpectedDistance;
                    if (nextCost < node.node.ExpectedTotalCost)
                    {
                        node.node.ExpectedTotalCost = nextCost;
                        node.node.TotalCost = _currentNode.TotalCost + node.distance;
                        node.node.previousVisited = _currentNode;
                        if (_endTest(node.node))
                        {
                            return (true, node.node);
                        }
                        if (!_nodesToCheck.Contains(node.node))
                        {
                            _nodesToCheck.Add(node.node);
                        }
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
                _nodesToCheck = _nodesToCheck.OrderBy(node => node.ExpectedTotalCost).ToList();
            }
            else
            {
                _nodesToCheck = _nodesToCheck.OrderByDescending(node => node.ExpectedTotalCost).ToList();
            }
            _currentNode = _nodesToCheck[0];
            return (false, _currentNode);
        }

        public static Node<T>? DoAstar(Node<T> startingNode, EndTest isEnd, bool descending = false)
        {
            AStar<T> dijkstra = new(startingNode, isEnd, descending);
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
