using System.Diagnostics;

namespace AdventOfCode.Day17
{
    /// <summary>
    /// Main Class for Challenge 2
    /// </summary>
    public static class Challenge2
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

            Day2State current = new((0, 0), 2, 0);
            List<Day2State> toExplore = [current];
            Dictionary<Day2State, int> prices = [];
            prices.Add(current, 0);
            List<Day2State> explored = [];

            //Same algorithm, but also checks whether we're in steps range
            while (current.CurrentPosition.X != width - 1 || current.CurrentPosition.Y != height - 1 || current.Steps <= Day2State.MinSteps)
            {
                explored.Add(current);
                foreach (Day2State state in current.NextStates())
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
    /// Contains state for part 2 and  generates next ones
    /// </summary>
    public class Day2State
    {
        public readonly (int X, int Y) CurrentPosition;
        public readonly int Direction;
        public readonly int Steps;
        public readonly Day2State? PreviousState = null;
        private static readonly int MaxSteps = 9; //Maximum steps. 0,1,2,.. 9 is tenth. I know. Confusing
        public static readonly int MinSteps = 2; //Minimum steps. 0,1,2,.. 3 is fourth. I know. Confusing

        public Day2State((int X, int Y) position, int direction, int steps)
        {
            CurrentPosition = position;
            Direction = direction;
            Steps = steps;
        }

        public Day2State(Day2State previousState, (int X, int Y) offset, int direction, int steps)
        {
            CurrentPosition = (previousState.CurrentPosition.X + offset.X, previousState.CurrentPosition.Y + offset.Y);
            Direction = direction;
            Steps = steps;
            PreviousState = previousState;
        }

        public List<Day2State> NextStates()
        {
            List<Day2State> states = [];
            switch (Direction)
            {
                case 0:
                    if (Steps < MaxSteps || PreviousState == null)
                    {
                        states.Add(new(this, (0, -1), 0, Steps + 1));
                    }
                    if (Steps > MinSteps || PreviousState == null)
                    {
                        states.Add(new(this, (1, 0), 1, 0));
                        states.Add(new(this, (-1, 0), 3, 0));
                    }
                    return states;
                case 1:
                    if (Steps < MaxSteps || PreviousState == null)
                    {
                        states.Add(new(this, (1, 0), 1, Steps + 1));
                    }
                    if (Steps > MinSteps || PreviousState == null)
                    {
                        states.Add(new(this, (0, 1), 2, 0));
                        states.Add(new(this, (0, -1), 0, 0));
                    }
                    return states;
                case 2:
                    if (Steps < MaxSteps || PreviousState == null)
                    {
                        states.Add(new(this, (0, 1), 2, Steps + 1));
                    }
                    if (Steps > MinSteps || PreviousState == null)
                    {
                        states.Add(new(this, (1, 0), 1, 0));
                        states.Add(new(this, (-1, 0), 3, 0));
                    }
                    return states;
                default:
                    if (Steps < MaxSteps || PreviousState == null)
                    {
                        states.Add(new(this, (-1, 0), 3, Steps + 1));
                    }
                    if (Steps > MinSteps || PreviousState == null)
                    {
                        states.Add(new(this, (0, 1), 2, 0));
                        states.Add(new(this, (0, -1), 0, 0));
                    }
                    return states;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not Day2State) return false;
            Day2State obj2 = (Day2State)obj;
            return (CurrentPosition.X == obj2.CurrentPosition.X && CurrentPosition.Y == obj2.CurrentPosition.Y && Direction == obj2.Direction && Steps == obj2.Steps);
        }

        public override int GetHashCode()
        {
            return CurrentPosition.X + CurrentPosition.Y + Direction + Steps;
        }
    }
}