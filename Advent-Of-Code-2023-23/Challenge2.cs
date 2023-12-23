using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace AdventOfCode.Day23
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
    {
        private static char[,] map = new char[0, 0];
        private static int width = 0;
        private static int height = 0;
        private static int start = 0;
        private static int end = 0;
        private static List<Junction> junctions = [];
        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');
            height = inputData.Length - 2;
            width = inputData[0].Length - 2;
            map = new char[width, height];

            for (int i = 0; i < width + 2; i++)
            {
                if (inputData[0][i] == '.') start = i - 1;
                if (inputData[height + 1][i] == '.') end = i - 1;
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = inputData[y + 1][x + 1];
                    /*Console.CursorLeft = x;
                    Console.CursorTop = y;
                    Console.Write(map[x, y]);*/
                }
            }

            junctions.Add(new (start, 0));

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (GetNext(new(x, y)).Count > 2)
                        junctions.Add(new(x, y));
                }
            }

            junctions.Add(new(end, height-1));
            
            for (int i = 0; i < junctions.Count; i++)
            {
                for (int j = i+1; j < junctions.Count; j++)
                {
                    List<Junction> visited = [];
                    List<Junction> longestPathSearch = [];
                    int path = FindLongestBetweenJunctions(junctions[i], junctions[j], [], ref visited, ref longestPathSearch, 0, true);
                    if (path > -1)
                    {
                        junctions[i].Connections.Add((junctions[j], path));
                        junctions[j].Connections.Add((junctions[i], path));
                    }
                }
            }

            List<Junction> longestPath = [];
            int length = DFS(junctions[0], [], ref longestPath, 0);

            return length + 2;
        }

        private static int DFS(Junction current, List<Junction> currentPath, ref List<Junction> longestPath, int currentLength)
        {
            int maxLength = 0;
            currentPath.Add(current);
            if (IsEnd(current))
            {
                if (currentPath.Count >= longestPath.Count)
                {
                    longestPath = new(currentPath);
                }
                return currentLength;
            }

            foreach ((Junction connection, int price) in current.Connections)
            {
                if (!currentPath.Contains(connection))
                {
                    int length = DFS(connection, new(currentPath), ref longestPath, currentLength + price);
                    if (length> maxLength) maxLength = length;
                }
            }

            currentPath.RemoveAt(currentPath.Count - 1);
            return maxLength;
        }

        private static bool IsEnd(Junction state)
        {
            return (state.X == end && state.Y == height - 1);
        }

        private static int FindLongestBetweenJunctions(Junction current, Junction end, List<Junction> currentPath, ref List<Junction> visited, ref List<Junction> longestPath, int currentLength, bool start = false)
        {
            visited.Add(current);

            if (current == end)
            {
                if (currentPath.Count >= longestPath.Count)
                {
                    longestPath = new(currentPath);
                }
                return longestPath.Count;
            }

            currentPath.Add(current);
            HashSet<Junction> next = GetNext(current);
            if (next.Count > 2 && !start)
                return -1;
            bool found = false;
            foreach (Junction nextState in next)
            {
                if (!visited.Contains(nextState))
                if (FindLongestBetweenJunctions(nextState, end, new(currentPath), ref visited, ref longestPath, currentLength + 1) != -1) found = true;
            }
            if (!found) return -1;
            currentPath.RemoveAt(currentPath.Count - 1);
            return longestPath.Count;
        }

        private static bool SafeAt(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height) return false;
            return map[x, y] != '#';
        }
        private static char GetAt(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height) return '#';
            return map[x, y];
        }

        private static void AddNextState(Junction state, int xOffset, int yOffset, ref HashSet<Junction> states)
        {
            Junction nextState = new(state.X + xOffset, state.Y + yOffset);
            if (SafeAt(nextState.X, nextState.Y))
            {
                states.Add(nextState);
            }
        }

        private static HashSet<Junction> GetNext(Junction state)
        {
            Junction startState = state;
            HashSet<Junction> states = [];
            if (GetAt(state.X, state.Y) != '#')
            {
                AddNextState(startState, +1, 0, ref states);
                AddNextState(startState, -1, 0, ref states);
                AddNextState(startState, 0, -1, ref states);
                AddNextState(startState, 0, +1, ref states);
            
            }
            return states;
        }
    }
}