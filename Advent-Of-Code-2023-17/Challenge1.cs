using System.Diagnostics;

namespace AdventOfCode.Day17
{
    /// <summary>
    /// Main Class for Challenge 1
    /// </summary>
    public static class Challenge1
    {
        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallenge(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            int width = inputData[0].Length;
            int height = inputData.Length;

            int[,] map = new int[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = int.Parse(inputData[y][x].ToString());
                }
            }

            Day1State current = new((0, 0), 2, 0);
            List<Day1State> toExplore = [current];
            Dictionary<Day1State, int> prices = [];
            prices.Add(current, 0);
            List<Day1State> explored = [];

            //Just standard pathfinding 
            while (current.CurrentPosition.X != width-1 || current.CurrentPosition.Y != height-1)
            {
                explored.Add(current);
                foreach (Day1State state in current.NextStates())
                {
                    //if already checked
                    if (explored.Contains(state))
                        continue;

                    //If possible
                    if (state.CurrentPosition.X < 0 || state.CurrentPosition.X >= width || state.CurrentPosition.Y < 0 || state.CurrentPosition.Y >= height)
                    {
                        continue;
                    }

                    int nextPrice = prices[current] + map[state.CurrentPosition.X, state.CurrentPosition.Y];

                    if (!toExplore.Contains(state))
                    {
                        if (!prices.TryAdd(state, nextPrice))
                        {
                            prices[state] = nextPrice;
                        }
                        toExplore.Add(state);
                    }
                    else
                    {
                        if (nextPrice < prices[state])
                        {
                            if (!prices.TryAdd(state, nextPrice))
                            {
                                prices[state] = nextPrice;
                            }
                        }
                    }
                }

                toExplore.RemoveAt(0);

                toExplore = toExplore.OrderBy((a) => { return prices[a]; }).ToList();
                current = toExplore[0];

            }
            return prices[current];
        }
    }

    /// <summary>
    /// Contains information about current state an is able to generate next states
    /// </summary>
    public class Day1State
    {
        public readonly (int X, int Y) CurrentPosition;
        public readonly int Direction;
        public readonly int Steps;
        public readonly Day1State? PreviousState = null;
        private static readonly int MaxSteps = 2; //Maximum steps. 0,1,2, 3 is fourth. I know. Confusing

        public Day1State((int X, int Y) position, int direction, int steps)
        {
            CurrentPosition = position;
            Direction = direction;
            Steps = steps;
        }

        public Day1State(Day1State previousState, (int X, int Y) offset, int direction, int steps)
        {
            CurrentPosition = (previousState.CurrentPosition.X + offset.X, previousState.CurrentPosition.Y + offset.Y);
            Direction = direction;
            Steps = steps;
            PreviousState = previousState;
        }

        public List<Day1State> NextStates()
        {
            List<Day1State> states = [];
            switch (Direction)
            {
                case 0:
                    if (Steps < MaxSteps)
                    {
                        states.Add(new (this, (0, -1), 0, Steps + 1));
                    }
                    states.Add(new (this, (1, 0), 1, 0));
                    states.Add(new (this, (-1, 0), 3, 0));
                    return states;
                case 1:
                    if (Steps < MaxSteps)
                    {
                        states.Add(new (this, (1, 0), 1, Steps + 1));
                    }
                    states.Add(new (this, (0, 1), 2, 0));
                    states.Add(new (this, (0, -1), 0, 0));
                    return states;
                case 2:
                    if (Steps < MaxSteps)
                    {
                        states.Add(new (this, (0, 1), 2, Steps + 1));
                    }
                    states.Add(new (this, (1, 0), 1, 0));
                    states.Add(new (this, (-1, 0), 3, 0));
                    return states;
                default:
                    if (Steps < MaxSteps)
                    {
                        states.Add(new (this, (-1, 0), 3, Steps + 1));
                    }
                    states.Add(new (this, (0, 1), 2, 0));
                    states.Add(new (this, (0, -1), 0, 0));
                    return states;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Day1State) return false;
            Day1State obj2 = (Day1State)obj;
            return (CurrentPosition.X == obj2.CurrentPosition.X && CurrentPosition.Y == obj2.CurrentPosition.Y && Direction == obj2.Direction && Steps == obj2.Steps);
        }

        public override int GetHashCode()
        {
            return CurrentPosition.X + CurrentPosition.Y + Direction + Steps;
        }
    }
}