using AdventOfCode.Day17;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2023_17
{
    internal class MiniDijkstra
    {
        /// <summary>
        /// Dijkstra implementation made just for this, because I'm lazy to fix ma actual prepared one and only returns final price.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="isEnd"></param>
        /// <param name="getStates"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int DoDijkstra (State startPoint, Func<State, bool> isEnd, Func<State, List<State>> getStates, ref int[,] map)
        {
            //Preparation
            State? current = startPoint;
            PriorityQueue<State, int> queue = new();
            Dictionary<State, (State parent, int distance)> parentDistances = [];
            parentDistances[current] = (current, 0);
            int currentDistance = 0;

            //While we're not at end
            while (current is not null && !isEnd(current))
            {
                if (parentDistances[current].distance < currentDistance)
                {
                    continue;
                }

                foreach (State nextState in getStates(current))
                {
                    //Don't ask me how this works
                    int nextDistance = currentDistance + map[nextState.CurrentPosition.X, nextState.CurrentPosition.Y];
                    if (!parentDistances.TryGetValue(nextState, out var parent) || (nextDistance < parent.distance))
                    {
                        parentDistances[nextState] = (current, nextDistance);
                        queue.Enqueue(nextState, nextDistance);
                    }
                }

                queue.TryDequeue(out current, out currentDistance);
            }

            //End
            return currentDistance;
        }
    }
}
