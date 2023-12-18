namespace AdventOfCode.Day17
{
    public class State
    {
        public readonly (int X, int Y) CurrentPosition;
        public readonly int Direction;
        public readonly int Steps;
        public readonly State? PreviousState = null;

        public State((int X, int Y) position, int direction, int steps)
        {
            CurrentPosition = position;
            Direction = direction;
            Steps = steps;
        }

        public State(State previousState, (int X, int Y) offset, int direction, int steps)
        {
            CurrentPosition = (previousState.CurrentPosition.X + offset.X, previousState.CurrentPosition.Y + offset.Y);
            Direction = direction;
            Steps = steps;
            PreviousState = previousState;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not State) return false;
            State obj2 = (State)obj;
            return (CurrentPosition.X == obj2.CurrentPosition.X && CurrentPosition.Y == obj2.CurrentPosition.Y && Direction == obj2.Direction && Steps == obj2.Steps);
        }

        public override int GetHashCode()
        {
            return CurrentPosition.X + CurrentPosition.Y + Direction + Steps;
        }
    }
}