using System.ComponentModel;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Day23
{
    /// <summary>
    /// Main Class for Challenge 1
    /// </summary>
    public static class Challenge1
    {
        private static char[,] map = new char[0,0];
        private static int width = 0;
        private static int height = 0;
        private static int start = 0;
        private static int end = 0;
        private static List<State> visited = [];
        private static List<State> longestPath = [];
        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            return -1; 
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
                }
            }            

            DFS(new(0, start), [], 0);

            return longestPath.Count + 1;
        }

        private static void DFS(State current, List<State> currentPath, int currentLength)
        {
            currentPath.Add(current);
            if (IsEnd(current))
            {
                if (currentPath.Count >= longestPath.Count)
                {
                    longestPath = new(currentPath);
                }
                return;
            }

            foreach (State nextState in GetNext(current))
            {
                DFS(nextState, new(currentPath), currentLength + 1);
            }

            currentPath.RemoveAt(currentPath.Count - 1);
        }


        private static bool IsStart(State state)
        {
            return (state.X == end && state.Y == height - 1);
        }

        private static bool IsEnd(State state)
        {
            return (state.X == end && state.Y == height - 1);
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

        private static void AddNextState(State state, int xOffset, int yOffset, ref HashSet<State> states)
        {
            State nextState = new(state.X + xOffset, state.Y + yOffset);
            if (SafeAt(nextState.X, nextState.Y) && !state.PreviousStates.Contains(nextState))
            {
                nextState.PreviousStates.AddRange(state.PreviousStates);
                nextState.PreviousStates.Add(state);
                states.Add(nextState);
            }
        }
        private static HashSet<State> GetNext(State state)
        {
            State startState = state;
            HashSet<State> states = [];
            switch (GetAt(state.X, state.Y))
            {
                case '.':
                    AddNextState(startState, +1, 0, ref states);
                    AddNextState(startState, -1, 0, ref states);
                    AddNextState(startState, 0, -1, ref states);
                    AddNextState(startState, 0, +1, ref states);
                    break;
                case '>':
                    AddNextState(startState, +1, 0, ref states);
                    break;
                case '<':
                    AddNextState(startState, -1, 0, ref states);
                    break;
                case '^':
                    AddNextState(startState, 0, -1, ref states);
                    break;
                case 'v':
                    AddNextState(startState, 0, +1, ref states);
                    break;
            }
            return states;
        }
    }
}