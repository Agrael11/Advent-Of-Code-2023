using Advent_Of_Code_2023_17;
using System.Diagnostics;

namespace AdventOfCode.Day17
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
    {
        private static readonly int MaxSteps = 9; //Maximum steps. 0,1,2,.. 9 is tenth. I know. Confusing
        public static readonly int MinSteps = 2; //Minimum steps. 0,1,2,.. 3 is fourth. I know. Confusing

        static int width = 0;
        static int height = 0;

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            width = inputData[0].Length;
            height = inputData.Length;

            int[,] map = new int[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = int.Parse(inputData[y][x].ToString());
                }
            }

            return MiniDijkstra.DoDijkstra(new State((0, 0), 2, 0), IsEnd, NextStates, ref map);
        }

        /// <summary>
        /// Checks if at end with required number of steps
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        static bool IsEnd(State state)
        {
            return (state.CurrentPosition.X == width - 1 && state.CurrentPosition.Y == height - 1 && state.Steps > MinSteps);
        }

        /// <summary>
        /// Generates next steps
        /// </summary>
        /// <param name="currentState"></param>
        /// <returns></returns>
        static List<State> NextStates(State currentState)
        {
            List<State> states = [];
            switch (currentState.Direction)
            {
                case 0:
                    if (currentState.Steps < MaxSteps || currentState.PreviousState == null)
                    {
                        AddToList(ref states, new(currentState, (0, -1), 0, currentState.Steps + 1));
                    }
                    if (currentState.Steps > MinSteps || currentState.PreviousState == null)
                    {
                        AddToList(ref states, new(currentState, (1, 0), 1, 0));
                        AddToList(ref states, new(currentState, (-1, 0), 3, 0));
                    }
                    return states;
                case 1:
                    if (currentState.Steps < MaxSteps || currentState.PreviousState == null)
                    {
                        AddToList(ref states, new(currentState, (1, 0), 1, currentState.Steps + 1));
                    }
                    if (currentState.Steps > MinSteps || currentState.PreviousState == null)
                    {
                        AddToList(ref states, new(currentState, (0, 1), 2, 0));
                        AddToList(ref states, new(currentState, (0, -1), 0, 0));
                    }
                    return states;
                case 2:
                    if (currentState.Steps < MaxSteps || currentState.PreviousState == null)
                    {
                        AddToList(ref states, new(currentState, (0, 1), 2, currentState.Steps + 1));
                    }
                    if (currentState.Steps > MinSteps || currentState.PreviousState == null)
                    {
                        AddToList(ref states, new(currentState, (1, 0), 1, 0));
                        AddToList(ref states, new(currentState, (-1, 0), 3, 0));
                    }
                    return states;
                default:
                    if (currentState.Steps < MaxSteps || currentState.PreviousState == null)
                    {
                        AddToList(ref states, new(currentState, (-1, 0), 3, currentState.Steps + 1));
                    }
                    if (currentState.Steps > MinSteps || currentState.PreviousState == null)
                    {
                        AddToList(ref states, new(currentState, (0, 1), 2, 0));
                        AddToList(ref states, new(currentState, (0, -1), 0, 0));
                    }
                    return states;
            }
        }

        private static void AddToList(ref List<State> list, State state)
        {
            if (state.CurrentPosition.X < 0 || state.CurrentPosition.Y < 0 || state.CurrentPosition.X >= width || state.CurrentPosition.Y >= height)
                return;
            list.Add(state);
        }
    }
}